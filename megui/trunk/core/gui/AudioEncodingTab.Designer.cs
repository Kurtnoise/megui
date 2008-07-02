namespace MeGUI.core.gui
{
    partial class AudioEncodingTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.audioContainer = new System.Windows.Forms.ComboBox();
            this.audioContainerLabel = new System.Windows.Forms.Label();
            this.queueAudioButton = new System.Windows.Forms.Button();
            this.audioInputLabel = new System.Windows.Forms.Label();
            this.audioOutputLabel = new System.Windows.Forms.Label();
            this.deleteAudioButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.delay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.audioProfile = new MeGUI.core.gui.ConfigableProfilesControl();
            this.cuts = new MeGUI.FileBar();
            this.audioOutput = new MeGUI.FileBar();
            this.audioInput = new MeGUI.FileBar();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.delay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Cuts";
            // 
            // audioContainer
            // 
            this.audioContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioContainer.FormattingEnabled = true;
            this.audioContainer.Location = new System.Drawing.Point(110, 119);
            this.audioContainer.Name = "audioContainer";
            this.audioContainer.Size = new System.Drawing.Size(85, 21);
            this.audioContainer.TabIndex = 25;
            this.audioContainer.SelectedIndexChanged += new System.EventHandler(this.audioContainer_SelectedIndexChanged);
            // 
            // audioContainerLabel
            // 
            this.audioContainerLabel.AutoSize = true;
            this.audioContainerLabel.Location = new System.Drawing.Point(3, 122);
            this.audioContainerLabel.Name = "audioContainerLabel";
            this.audioContainerLabel.Size = new System.Drawing.Size(56, 13);
            this.audioContainerLabel.TabIndex = 24;
            this.audioContainerLabel.Text = "Extension ";
            // 
            // queueAudioButton
            // 
            this.queueAudioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queueAudioButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.queueAudioButton.Location = new System.Drawing.Point(393, 146);
            this.queueAudioButton.Name = "queueAudioButton";
            this.queueAudioButton.Size = new System.Drawing.Size(60, 23);
            this.queueAudioButton.TabIndex = 27;
            this.queueAudioButton.Text = "Enqueue";
            this.queueAudioButton.Click += new System.EventHandler(this.queueAudioButton_Click);
            // 
            // audioInputLabel
            // 
            this.audioInputLabel.AutoSize = true;
            this.audioInputLabel.Location = new System.Drawing.Point(3, 10);
            this.audioInputLabel.Name = "audioInputLabel";
            this.audioInputLabel.Size = new System.Drawing.Size(61, 13);
            this.audioInputLabel.TabIndex = 15;
            this.audioInputLabel.Text = "Audio Input";
            // 
            // audioOutputLabel
            // 
            this.audioOutputLabel.AutoSize = true;
            this.audioOutputLabel.Location = new System.Drawing.Point(3, 65);
            this.audioOutputLabel.Name = "audioOutputLabel";
            this.audioOutputLabel.Size = new System.Drawing.Size(69, 13);
            this.audioOutputLabel.TabIndex = 19;
            this.audioOutputLabel.Text = "Audio Output";
            // 
            // deleteAudioButton
            // 
            this.deleteAudioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAudioButton.Location = new System.Drawing.Point(339, 146);
            this.deleteAudioButton.Name = "deleteAudioButton";
            this.deleteAudioButton.Size = new System.Drawing.Size(48, 23);
            this.deleteAudioButton.TabIndex = 26;
            this.deleteAudioButton.Text = "X";
            this.deleteAudioButton.Click += new System.EventHandler(this.deleteAudioButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Delay";
            // 
            // delay
            // 
            this.delay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delay.Location = new System.Drawing.Point(268, 120);
            this.delay.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.delay.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.delay.Name = "delay";
            this.delay.Size = new System.Drawing.Size(136, 20);
            this.delay.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "ms";
            // 
            // audioProfile
            // 
            this.audioProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.audioProfile.Location = new System.Drawing.Point(110, 89);
            this.audioProfile.Name = "audioProfile";
            this.audioProfile.ProfileSet = "Audio";
            this.audioProfile.Size = new System.Drawing.Size(343, 22);
            this.audioProfile.TabIndex = 31;
            this.audioProfile.SelectedProfileChanged += new System.EventHandler(this.audioProfile_SelectedProfileChanged);
            // 
            // cuts
            // 
            this.cuts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cuts.Filename = "";
            this.cuts.Filter = "MeGUI cutlist files (*.clt)|*.clt";
            this.cuts.FolderMode = false;
            this.cuts.Location = new System.Drawing.Point(110, 32);
            this.cuts.Name = "cuts";
            this.cuts.ReadOnly = true;
            this.cuts.SaveMode = false;
            this.cuts.Size = new System.Drawing.Size(343, 26);
            this.cuts.TabIndex = 18;
            this.cuts.Title = "Select a file with cuts";
            // 
            // audioOutput
            // 
            this.audioOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.audioOutput.Filename = "";
            this.audioOutput.Filter = null;
            this.audioOutput.FolderMode = false;
            this.audioOutput.Location = new System.Drawing.Point(110, 59);
            this.audioOutput.Name = "audioOutput";
            this.audioOutput.ReadOnly = false;
            this.audioOutput.SaveMode = true;
            this.audioOutput.Size = new System.Drawing.Size(343, 29);
            this.audioOutput.TabIndex = 20;
            this.audioOutput.Title = "Enter name of output";
            // 
            // audioInput
            // 
            this.audioInput.AllowDrop = true;
            this.audioInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.audioInput.Filename = "";
            this.audioInput.Filter = "All files (*.*)|*.*";
            this.audioInput.FolderMode = false;
            this.audioInput.Location = new System.Drawing.Point(110, 4);
            this.audioInput.Name = "audioInput";
            this.audioInput.ReadOnly = true;
            this.audioInput.SaveMode = false;
            this.audioInput.Size = new System.Drawing.Size(343, 29);
            this.audioInput.TabIndex = 16;
            this.audioInput.Title = "Select your audio input";
            this.audioInput.FileSelected += new MeGUI.FileBarEventHandler(this.audioInput_FileSelected);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Encoder settings";
            // 
            // AudioEncodingTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.audioProfile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.delay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cuts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.audioOutput);
            this.Controls.Add(this.audioInput);
            this.Controls.Add(this.audioContainer);
            this.Controls.Add(this.audioContainerLabel);
            this.Controls.Add(this.queueAudioButton);
            this.Controls.Add(this.audioInputLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.audioOutputLabel);
            this.Controls.Add(this.deleteAudioButton);
            this.Name = "AudioEncodingTab";
            this.Size = new System.Drawing.Size(456, 173);
            ((System.ComponentModel.ISupportInitialize)(this.delay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FileBar cuts;
        private System.Windows.Forms.Label label1;
        private FileBar audioOutput;
        private FileBar audioInput;
        private System.Windows.Forms.ComboBox audioContainer;
        private System.Windows.Forms.Label audioContainerLabel;
        private System.Windows.Forms.Button queueAudioButton;
        private System.Windows.Forms.Label audioInputLabel;
        private System.Windows.Forms.Label audioOutputLabel;
        private System.Windows.Forms.Button deleteAudioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown delay;
        private System.Windows.Forms.Label label3;
        private ConfigableProfilesControl audioProfile;
        private System.Windows.Forms.Label label4;
    }
}
