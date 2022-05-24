using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Speaker;
using NAudio.Utils;
using NAudio.Wave;
using System.Configuration;
using System.Diagnostics;
using System.Media;
using System.Reflection;

namespace VerificationDemo
{
    public partial class MainWindow : Form
    {
        bool isRecording = false;
        bool isEnrollment = true;

        private VoiceProfileClient? enrollmentClient;
        private WaveInEvent? waveIn;
        private WaveFileWriter? fileWriter;
        private Stream? audioStream;

        private static string subscriptionKey = "";
        private static string mode = "";
        private VoiceProfile? profile = null;
        private readonly string recordingDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Recordings");
        private string fileName = string.Empty;
        private SpeechConfig? speechConfig;
        private string region = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Load(object? sender, EventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(27, 26, 32);
            this.textBox1.BackColor = Color.FromArgb(27, 26, 32);
            this.textBox1.ForeColor = Color.White;


            var profileType = VoiceProfileType.TextIndependentIdentification;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings.AllKeys.Contains("Mode"))
            {
                mode = config.AppSettings.Settings["Mode"].Value.ToString();
                if (mode.Equals("TDSV"))
                {
                    profileType = VoiceProfileType.TextDependentVerification;
                }
                else if (mode.Equals("TISV"))
                {
                    profileType = VoiceProfileType.TextIndependentVerification;
                }
                else
                {
                    throw new ArgumentException($"Unsupported mode: {mode}.");
                }
            }
            else
            {
                throw new ArgumentException("Mode setting not present in App.config settings.");
            }

            if (config.AppSettings.Settings.AllKeys.Contains("SubscriptionKey"))
            {
                subscriptionKey = config.AppSettings.Settings["SubscriptionKey"].Value.ToString();
            }
            else
            {
                throw new ArgumentException("SubscriptionKey setting not present in App.config settings.");
            }

            if (config.AppSettings.Settings.AllKeys.Contains("Region"))
            {
                region = config.AppSettings.Settings["Region"].Value.ToString();
            }
            else
            {
                throw new ArgumentException("Region setting not present in App.config settings.");
            }

            this.speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);
            this.enrollmentClient = new VoiceProfileClient(speechConfig);

            if (profile == null)
            {
                this.WriteLog("Creating speaker profile", MessageType.Info);

                try
                {
                    this.profile = await this.enrollmentClient.CreateProfileAsync(profileType, "en-us").ConfigureAwait(false);
                }
                catch(Exception ex)
                {
                    this.WriteLog($"Profile creation failed. Please make sure you provided valid subscription key and region.", MessageType.Error);
                    throw new ArgumentException("Profile creation failed. Please make sure you provided valid subscription key and region.", ex);
                }
            }

            if (this.profile != null)
            {
                this.WriteLog($"Profile ID: {this.profile.Id}", MessageType.Info);
            }

            InitializeRecorder();

            if (!Directory.Exists(recordingDir))
            {
                Directory.CreateDirectory(recordingDir);
            }
        }

        private void InitializeRecorder()
        {
            waveIn = new WaveInEvent { DeviceNumber = 0 };
            int sampleRate = 16000;
            int channels = 1;
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.WaveFormat = new WaveFormat(sampleRate, channels);
            waveIn.RecordingStopped += WaveSource_RecordingStopped;
        }

        private void EnrollBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRecording)
                {
                    isRecording = false;
                    waveIn.StopRecording();
                    this.WriteLog("Stopped recording", MessageType.Info);

                    this.enrollBtn.Text = "Enroll from Mic";
                    this.verifyBtn.Enabled = true;
                }
                else
                {
                    isRecording = true;
                    isEnrollment = true;

                    waveIn.StartRecording();
                    this.WriteLog("Started recording...", MessageType.Info);
                    this.enrollBtn.Text = "Stop Recording";
                    this.verifyBtn.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }

        private async void EnrollFileBtn_Click(object sender, EventArgs e)
        {
            string filePath = GetFilePath();
            if (!string.IsNullOrEmpty(filePath))
            {
                await this.AddEnrollment(filePath);
            }
        }

        private static string GetFilePath()
        {
            string filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "WAV files (*.wav)|*.wav";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            return filePath;
        }

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRecording)
                {
                    isRecording = false;
                    waveIn.StopRecording();
                    this.WriteLog("Stopped recording", MessageType.Info);

                    this.verifyBtn.Text = "Recognize from Mic";
                    this.enrollBtn.Enabled = true;
                }
                else
                {
                    isRecording = true;
                    isEnrollment = false;

                    waveIn.StartRecording();
                    this.WriteLog("Started recording...", MessageType.Info);
                    this.verifyBtn.Text = "Stop Recording";
                    this.enrollBtn.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }

        private async void RecognizeFileBtn_Click(object sender, EventArgs e)
        {
            string filePath = GetFilePath();
            if (!string.IsNullOrEmpty(filePath))
            {
                await this.Recognize(filePath);
            }
        }

        private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
        {
            if (fileWriter == null)
            {
                audioStream = new IgnoreDisposeStream(new MemoryStream());
                fileWriter = new WaveFileWriter(audioStream, waveIn.WaveFormat);
            }

            fileWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }

        private async void WaveSource_RecordingStopped(object? sender, StoppedEventArgs _)
        {
            fileWriter?.Dispose();
            fileWriter = null;

            fileName = Path.Combine(recordingDir, DateTime.Now.Ticks.ToString() + ".wav");
            using (var fileStream = File.Create(Path.Combine(fileName)))
            {
                audioStream.Seek(0, SeekOrigin.Begin);
                audioStream.CopyTo(fileStream);
            }

            //audioStream.Seek(0, SeekOrigin.Begin);

            //SoundPlayer p = new SoundPlayer();
            //p.Stream = audioStream;
            //p.Play();

            audioStream.Seek(0, SeekOrigin.Begin);
            waveIn.Dispose();
            InitializeRecorder();

            try
            {
                if (isEnrollment)
                {
                    await AddEnrollment(fileName);
                }
                else
                {
                    await Recognize(fileName);
                }
            }
            catch (Exception exception)
            {
                if (!isEnrollment)
                {
                    this.resultTxtBox.Text = "";
                    this.resultTxtBox.BackColor = Color.Gray;
                    this.resultPanel.BackColor = Color.Gray;
                }

                this.HandleException(exception);
            }
        }

        private async Task Recognize(string filePath)
        {
            this.WriteLog("Recognizing speaker...", MessageType.Info);

            using var audioInput = AudioConfig.FromWavFileInput(filePath);
            using var recognizer = new SpeakerRecognizer(speechConfig, audioInput);
            using var model = SpeakerVerificationModel.FromProfile(this.profile);
            
            Stopwatch sw = Stopwatch.StartNew();

            var verificationResult = await recognizer.RecognizeOnceAsync(model);
            var elapsed = sw.Elapsed.TotalMilliseconds;

            this.Invoke(new MethodInvoker(delegate
            {
                if (verificationResult.Reason == ResultReason.RecognizedSpeaker)
                {
                    this.resultTxtBox.Text = "Verified";
                    this.resultTxtBox.BackColor = Color.Green;
                    this.resultPanel.BackColor = Color.Green;

                    this.WriteLog($"Speaker verified. Score: {verificationResult.Score}", MessageType.Info);
                }
                else if (verificationResult.Reason == ResultReason.NoMatch)
                {
                    this.resultTxtBox.Text = "Rejected";
                    this.resultTxtBox.BackColor = Color.Red;
                    this.resultPanel.BackColor = Color.Red;

                    this.WriteLog($"Speaker rejected. Score: {verificationResult.Score}", MessageType.Info);
                }
                else if (verificationResult.Reason == ResultReason.Canceled)
                {
                    var cancellation = SpeakerRecognitionCancellationDetails.FromResult(verificationResult);

                    this.resultTxtBox.Text = "Operation Failed";
                    this.resultTxtBox.BackColor = Color.Red;
                    this.resultPanel.BackColor = Color.Red;

                    var errorDescription = string.Empty;
                    if (!string.IsNullOrWhiteSpace(cancellation.ErrorDetails))
                    {
                        errorDescription = cancellation.ErrorDetails.Split('.')[0];
                    }

                    this.WriteLog($"{cancellation.ErrorCode} {errorDescription}", MessageType.Error);
                }

                    // this.latencyTxtBox.Text = $"{Math.Round(elapsed, 0)} ms";
                }));
        }

        private async Task AddEnrollment(string filePath)
        {
            this.WriteLog("Adding enrollment ...", MessageType.Info);

            using var audioInput = AudioConfig.FromWavFileInput(filePath);
            var result = await enrollmentClient.EnrollProfileAsync(this.profile, audioInput).ConfigureAwait(false);

            if (result.Reason == ResultReason.Canceled)
            {
                var cancellationDetails = VoiceProfileEnrollmentCancellationDetails.FromResult(result);

                var errorDescription = string.Empty;
                if (!string.IsNullOrWhiteSpace(cancellationDetails.ErrorDetails))
                {
                    var split = cancellationDetails.ErrorDetails.Split('.');
                    if(split.Length > 1)
                    {
                        errorDescription = cancellationDetails.ErrorDetails.Split('.')[1];
                    }
                    else
                    {
                        errorDescription = cancellationDetails.ErrorDetails.Split('.')[0];
                    }
                }

                this.WriteLog($"{cancellationDetails.ErrorCode} {errorDescription}", MessageType.Info);
            }
            else
            {
                this.WriteLog($"Added new enrollment.", MessageType.Info);

                // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate
                {
                    if (result.Reason == ResultReason.EnrollingVoiceProfile)
                    {
                        if (result.RemainingEnrollmentsSpeechLength.HasValue)
                        {
                            this.statusTxtBox.Text = $"Remaining: {result.RemainingEnrollmentsSpeechLength.Value.TotalSeconds} s";
                        }
                        else
                        {
                            this.statusTxtBox.Text = $"Remaining: {result.RemainingEnrollmentsCount} audios";
                        }
                    }
                    else if (result.Reason == ResultReason.EnrolledVoiceProfile)
                    {
                        this.statusTxtBox.Text = $"Enrolled";
                    }
                }));
            }
        }

        private void HandleException(Exception exception)
        {
            this.WriteLog($"Couldn't complete operation: {exception.Message}", MessageType.Error);
        }

        private void WriteLog(string message, MessageType type)
        {
            string finalMessage = string.Empty;
            switch (type)
            {
                case MessageType.Info:
                    finalMessage = "-i: ";
                    break;
                case MessageType.Warning:
                    finalMessage = "-w: ";
                    SystemSounds.Exclamation.Play();
                    break;
                case MessageType.Error:
                    finalMessage = "-e: ";
                    SystemSounds.Hand.Play();
                    break;
                default:
                    break;
            }

            finalMessage += message;
            this.Invoke((MethodInvoker)delegate
            {
                this.logTxtBox.AppendText(finalMessage + Environment.NewLine);
            });
        }

        enum MessageType
        {
            Info,
            Warning,
            Error
        }

        private async void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.enrollmentClient != null && this.profile != null)
            {
                await this.enrollmentClient.DeleteProfileAsync(this.profile);
            }
        }

        private async void ActivationPhrases_Click(object sender, EventArgs e)
        {
            this.WriteLog("Activation Phrases:", MessageType.Info);
            var phrases = await this.enrollmentClient.GetActivationPhrasesAsync(this.profile.Type, "en-us");

            foreach(var phrase in phrases.Phrases)
            {
                this.WriteLog($"-- {phrase}", MessageType.Info);
            }
        }
    }
}
