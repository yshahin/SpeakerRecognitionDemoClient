
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Speaker;

namespace TextIndependentVerificationDemo
{
    class Program
    {
        // Usage:
        //  app.exe [key] [region]
        //  with:
        //    [key] - optional - Speech Service subscription key to use
        //             (required if not hard-coded)
        //    [region] - optional - Azure service region corresponding to the key
        //                (required if not hard-coded)
        static async Task Main(string[] args)
        {
            string ArgOrDefault(int i, string defaultValue) => args.Length > i ? args[i] : defaultValue;

            // Remember to remove the key from your code when you're done, and never post it publicly.
            // For production, use a secure way of storing and accessing your credentials like Azure Key Vault.
            // See the Cognitive Services security article (https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-security) for more information.
            var key = ArgOrDefault(0, "<replace_with_your_key>");
            var region = ArgOrDefault(1, "<replace_with_your_region");

            // Activation phrase audio "I'll talk for a few seconds so you can recognize my voice in the future."
            var activationFilePath = "activation.wav";
            // Enrollment audio, any audio. Preferibly the phrase you want to use in the recognition.
            var enrollmentFilePath = "enrollment.wav";
            // Verification audio.
            var verificationFilePath = "verification.wav";
            // Same voice incorrect phrase
            var incorrectVerificationFilePath = "incorrect_verification.wav";

            if(!File.Exists(activationFilePath)) throw new FileNotFoundException($"Audio ${activationFilePath} not found");
            if(!File.Exists(enrollmentFilePath)) throw new FileNotFoundException($"Audio ${enrollmentFilePath} not found");
            if(!File.Exists(verificationFilePath)) throw new FileNotFoundException($"Audio ${verificationFilePath} not found");

            // Create a speech config to use with both SpeechRecognizer and SpeakerRecognizer.
            var speechConfig = SpeechConfig.FromSubscription(key, region);
            speechConfig.OutputFormat = OutputFormat.Detailed;

            // Create audio configs for the enrollment audio and verification audio.
            // File input is used in this sample, but any other options can be used
            // (e.g., DefaultMicrophoneInput, PushStream, PullStream, etc.)
            var activationAudioConfig = AudioConfig.FromWavFileInput(activationFilePath);
            var enrollmentAudioConfig = AudioConfig.FromWavFileInput(enrollmentFilePath);
            var verificationAudioConfig = AudioConfig.FromWavFileInput(verificationFilePath);
            var incorrectVerificationAudioConfig = AudioConfig.FromWavFileInput(incorrectVerificationFilePath);

            var test = await RecognizeSpeech(speechConfig, activationAudioConfig);
            Console.WriteLine($"Recognized activation text: {test.Text}");

            using (var client = new VoiceProfileClient(speechConfig)){
                using (var profile = await client.CreateProfileAsync(VoiceProfileType.TextIndependentVerification, "en-us").ConfigureAwait(false)){

                    // Create profile object with id, if it has been already created.
                    // var profile = new VoiceProfile(enrollmentResult.ProfileId, VoiceProfileType.TextIndependentVerification);

                    try
                    {
                        // First enrollment has to be don using the activation phrase. This happens only once
                        var activationResult = await ActivationEnroll(client, profile, activationAudioConfig);

                        // All other enrollments happen with any audio, this can happen as many times as needed
                        // until we have 0 remaining audio (Enrollment status is Enrolled)
                        var enrollmentResult = await VerificationEnroll(client, profile, enrollmentAudioConfig);

                        if (enrollmentResult.Reason == ResultReason.EnrolledVoiceProfile)
                        {
                            // Should return matched text and score > 0.5
                            await VerifyAudio(speechConfig, verificationAudioConfig, profile);

                            // Should return score > 0.5 and text should not match
                            await VerifyAudio(speechConfig, incorrectVerificationAudioConfig, profile);
                        }
                        else if (enrollmentResult.Reason == ResultReason.Canceled)
                        {
                            var cancellation = VoiceProfileEnrollmentCancellationDetails.FromResult(enrollmentResult);
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode} ErrorDetails={cancellation.ErrorDetails}");
                        }
                    }
                    finally
                    {
                        var result = await client.DeleteProfileAsync(profile);
                        Console.WriteLine($"Profile {profile.Id} cleaned up with status {result.Reason}");
                    }
                }
            }
        }

        public static async Task VerifyAudio(SpeechConfig speechConfig, AudioConfig audioConfig, VoiceProfile profile) {

            // Phrase that will be used an compared against.
            var customPassPhrase = "This is a custom phrase for verification.";

            // Speaker recognition threshold
            var similarityScoreThreshold = 0.5;

            // Start speech recognition and speaker verification in parallel
            var speechRecoTask = RecognizeSpeech(speechConfig, audioConfig);
            var speakerRecoTask = VerifySpeaker(speechConfig, audioConfig, profile);
            Task.WaitAll(speechRecoTask, speakerRecoTask);

            // Wait for both tasks to complete
            var speechRecoResult = await speechRecoTask;
            var speakerRecoResult = await speakerRecoTask;

            // Check for the correct phrase from speech recognition results
            // and minimum similarity score threshold from speaker verification results.
            // Combined, this can form custom text-dependent verification.
            if (speechRecoResult.Reason == ResultReason.RecognizedSpeech)
            {
                if (!speechRecoResult.Text.Equals(customPassPhrase)) {
                    Console.WriteLine($"Profile {profile.Id} did not match with phrase: \"{speechRecoResult.Text}\" and similarity score: {speakerRecoResult.Score}");
                } else if (speakerRecoResult.Score < similarityScoreThreshold) {
                    Console.WriteLine($"Profile {profile.Id} matched with phrase: \"{speechRecoResult.Text}\" but similarity score: {speakerRecoResult.Score} is less that threashold");
                } else {
                    Console.WriteLine($"Profile {profile.Id} matched with phrase: \"{speechRecoResult.Text}\" and similarity score: {speakerRecoResult.Score}");
                }
            }
            else if (speechRecoResult.Reason == ResultReason.Canceled)
            {
                Console.WriteLine(String.Format("Canceled, details: {0}", CancellationDetails.FromResult(speechRecoResult).ErrorDetails));
            }
        }

        public static async Task<VoiceProfileEnrollmentResult> ActivationEnroll(VoiceProfileClient client, VoiceProfile profile, AudioConfig audioConfig)
        {
            var phraseResult = await client.GetActivationPhrasesAsync(VoiceProfileType.TextIndependentVerification, "en-us").ConfigureAwait(false);

            Console.WriteLine($"Enrolling profile id {profile.Id}.");

            Console.WriteLine($"Speak the activation phrase, \"{phraseResult.Phrases[0]}\"");
            VoiceProfileEnrollmentResult result = await client.EnrollProfileAsync(profile, audioConfig).ConfigureAwait(false);
            Console.WriteLine($"Remaining enrollment audio time needed: {result}");
            Console.WriteLine("");

            if(result.Reason == ResultReason.Canceled) {
                throw new Exception($"{result}");
            }

            return result;
        }

        public static async Task<VoiceProfileEnrollmentResult> VerificationEnroll(VoiceProfileClient client, VoiceProfile profile, AudioConfig audioConfig)
        {
            var phraseResult = await client.GetActivationPhrasesAsync(VoiceProfileType.TextIndependentVerification, "en-us").ConfigureAwait(false);

            Console.WriteLine($"Enrolling profile id {profile.Id}.");

            VoiceProfileEnrollmentResult? result = null;
            while (result is null || result.RemainingEnrollmentsSpeechLength > TimeSpan.Zero)
            {
                result = await client.EnrollProfileAsync(profile, audioConfig).ConfigureAwait(false);
                Console.WriteLine($"Remaining enrollment audio time needed: {result}");
                Console.WriteLine("");
            }

            return result ?? throw new Exception("Failed to enroll verification audio");
        }

        public static async Task<SpeakerRecognitionResult> VerifySpeaker(SpeechConfig speechConfig, AudioConfig audioConfig, VoiceProfile profile)
        {
            var speakerRecognizer = new SpeakerRecognizer(speechConfig, audioConfig);
            var model = SpeakerVerificationModel.FromProfile(profile);
            var result = await speakerRecognizer.RecognizeOnceAsync(model).ConfigureAwait(false);
            return result;
        }

        public static async Task<SpeechRecognitionResult> RecognizeSpeech(SpeechConfig speechConfig, AudioConfig audioConfig)
        {
            var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = await speechRecognizer.RecognizeOnceAsync().ConfigureAwait(false);
            return result;
        }
    }
}
