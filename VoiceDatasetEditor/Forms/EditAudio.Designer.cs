namespace VoiceDatasetEditor.Forms
{
    partial class EditAudio
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
            panel1 = new Panel();
            btnPlay = new Button();
            btnSave = new Button();
            btnDiscard = new Button();
            btnStop = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(584, 173);
            panel1.TabIndex = 0;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.Location = new Point(12, 179);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(67, 26);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(468, 179);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(104, 26);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save changes";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnDiscard
            // 
            btnDiscard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDiscard.Location = new Point(345, 179);
            btnDiscard.Name = "btnDiscard";
            btnDiscard.Size = new Size(117, 26);
            btnDiscard.TabIndex = 3;
            btnDiscard.Text = "Discard changes";
            btnDiscard.UseVisualStyleBackColor = true;
            btnDiscard.Click += btnDiscard_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(85, 179);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(58, 26);
            btnStop.TabIndex = 4;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // EditAudio
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 217);
            Controls.Add(btnStop);
            Controls.Add(btnDiscard);
            Controls.Add(btnSave);
            Controls.Add(btnPlay);
            Controls.Add(panel1);
            MinimumSize = new Size(358, 145);
            Name = "EditAudio";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Edit Audio";
            FormClosed += EditAudio_FormClosed;
            Load += EditAudio_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnPlay;
        private Button btnSave;
        private Button btnDiscard;
        private Button btnStop;
    }
}