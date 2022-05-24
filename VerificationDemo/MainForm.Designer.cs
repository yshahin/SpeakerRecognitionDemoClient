namespace VerificationDemo
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.enrollBtn = new System.Windows.Forms.Button();
            this.verifyBtn = new System.Windows.Forms.Button();
            this.logTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.activationPhrases = new System.Windows.Forms.Button();
            this.enrollFileBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.recognizeFileBtn = new System.Windows.Forms.Button();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.resultTxtBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.resultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1124, 128);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(181, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(742, 55);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Azure Speaker Recognition Demo";
            // 
            // enrollBtn
            // 
            this.enrollBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.enrollBtn.Location = new System.Drawing.Point(7, 36);
            this.enrollBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.enrollBtn.Name = "enrollBtn";
            this.enrollBtn.Size = new System.Drawing.Size(330, 42);
            this.enrollBtn.TabIndex = 1;
            this.enrollBtn.Text = "Enroll from Mic";
            this.enrollBtn.UseVisualStyleBackColor = true;
            this.enrollBtn.Click += new System.EventHandler(this.EnrollBtn_Click);
            // 
            // verifyBtn
            // 
            this.verifyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.verifyBtn.Location = new System.Drawing.Point(22, 36);
            this.verifyBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.verifyBtn.Name = "verifyBtn";
            this.verifyBtn.Size = new System.Drawing.Size(330, 42);
            this.verifyBtn.TabIndex = 2;
            this.verifyBtn.Text = "Recognize from Mic";
            this.verifyBtn.UseVisualStyleBackColor = true;
            this.verifyBtn.Click += new System.EventHandler(this.VerifyBtn_Click);
            // 
            // logTxtBox
            // 
            this.logTxtBox.BackColor = System.Drawing.SystemColors.Menu;
            this.logTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTxtBox.Location = new System.Drawing.Point(2, 20);
            this.logTxtBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.logTxtBox.Multiline = true;
            this.logTxtBox.Name = "logTxtBox";
            this.logTxtBox.ReadOnly = true;
            this.logTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTxtBox.Size = new System.Drawing.Size(365, 296);
            this.logTxtBox.TabIndex = 3;
            this.logTxtBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logTxtBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(744, 142);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(369, 319);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation Log";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.activationPhrases);
            this.groupBox2.Controls.Add(this.enrollFileBtn);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.enrollBtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(9, 142);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(346, 324);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enrollment";
            // 
            // activationPhrases
            // 
            this.activationPhrases.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.activationPhrases.Location = new System.Drawing.Point(7, 129);
            this.activationPhrases.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.activationPhrases.Name = "activationPhrases";
            this.activationPhrases.Size = new System.Drawing.Size(330, 42);
            this.activationPhrases.TabIndex = 4;
            this.activationPhrases.Text = "Phrases";
            this.activationPhrases.UseVisualStyleBackColor = true;
            this.activationPhrases.Click += new System.EventHandler(this.ActivationPhrases_Click);
            // 
            // enrollFileBtn
            // 
            this.enrollFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.enrollFileBtn.Location = new System.Drawing.Point(7, 81);
            this.enrollFileBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.enrollFileBtn.Name = "enrollFileBtn";
            this.enrollFileBtn.Size = new System.Drawing.Size(330, 42);
            this.enrollFileBtn.TabIndex = 3;
            this.enrollFileBtn.Text = "Enroll from File";
            this.enrollFileBtn.UseVisualStyleBackColor = true;
            this.enrollFileBtn.Click += new System.EventHandler(this.EnrollFileBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.statusTxtBox);
            this.panel2.Location = new System.Drawing.Point(7, 177);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 139);
            this.panel2.TabIndex = 2;
            // 
            // statusTxtBox
            // 
            this.statusTxtBox.BackColor = System.Drawing.SystemColors.Menu;
            this.statusTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusTxtBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.statusTxtBox.Location = new System.Drawing.Point(2, 54);
            this.statusTxtBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.statusTxtBox.Name = "statusTxtBox";
            this.statusTxtBox.Size = new System.Drawing.Size(326, 26);
            this.statusTxtBox.TabIndex = 0;
            this.statusTxtBox.Text = "New Speaker";
            this.statusTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.recognizeFileBtn);
            this.groupBox3.Controls.Add(this.resultPanel);
            this.groupBox3.Controls.Add(this.verifyBtn);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(365, 142);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Size = new System.Drawing.Size(371, 324);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Recognition";
            // 
            // recognizeFileBtn
            // 
            this.recognizeFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recognizeFileBtn.Location = new System.Drawing.Point(22, 81);
            this.recognizeFileBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.recognizeFileBtn.Name = "recognizeFileBtn";
            this.recognizeFileBtn.Size = new System.Drawing.Size(330, 42);
            this.recognizeFileBtn.TabIndex = 3;
            this.recognizeFileBtn.Text = "Recognize from File";
            this.recognizeFileBtn.UseVisualStyleBackColor = true;
            this.recognizeFileBtn.Click += new System.EventHandler(this.RecognizeFileBtn_Click);
            // 
            // resultPanel
            // 
            this.resultPanel.Controls.Add(this.resultTxtBox);
            this.resultPanel.Location = new System.Drawing.Point(22, 137);
            this.resultPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(330, 179);
            this.resultPanel.TabIndex = 0;
            // 
            // resultTxtBox
            // 
            this.resultTxtBox.BackColor = System.Drawing.SystemColors.Menu;
            this.resultTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultTxtBox.ForeColor = System.Drawing.SystemColors.Window;
            this.resultTxtBox.Location = new System.Drawing.Point(48, 67);
            this.resultTxtBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.resultTxtBox.Name = "resultTxtBox";
            this.resultTxtBox.Size = new System.Drawing.Size(234, 26);
            this.resultTxtBox.TabIndex = 0;
            this.resultTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1123, 475);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Azure Cognitive Services";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button enrollBtn;
        private System.Windows.Forms.Button verifyBtn;
        private System.Windows.Forms.TextBox logTxtBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.TextBox resultTxtBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox statusTxtBox;
        private System.Windows.Forms.Button enrollFileBtn;
        private System.Windows.Forms.Button recognizeFileBtn;
        private Button activationPhrases;
    }
}

