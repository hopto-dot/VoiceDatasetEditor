namespace VoiceDatasetEditor
{
    partial class VoiceFile
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
            btnPlay = new Button();
            tbxTranscription = new TextBox();
            btnSave = new Button();
            lblLength = new Label();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // btnPlay
            // 
            btnPlay.Font = new Font("Segoe UI", 9.818182F);
            btnPlay.Location = new Point(17, 16);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(66, 35);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // tbxTranscription
            // 
            tbxTranscription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxTranscription.Font = new Font("Segoe UI", 13.7454548F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxTranscription.Location = new Point(89, 16);
            tbxTranscription.MaxLength = 5000;
            tbxTranscription.Name = "tbxTranscription";
            tbxTranscription.Size = new Size(722, 35);
            tbxTranscription.TabIndex = 1;
            tbxTranscription.WordWrap = false;
            tbxTranscription.TextChanged += tbxTranscription_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Font = new Font("Segoe UI", 9.818182F);
            btnSave.Location = new Point(901, 16);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 35);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblLength
            // 
            lblLength.Location = new Point(4, 50);
            lblLength.Name = "lblLength";
            lblLength.Size = new Size(93, 18);
            lblLength.TabIndex = 3;
            lblLength.Text = "0 seconds";
            lblLength.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.BackColor = SystemColors.Control;
            btnDelete.FlatStyle = FlatStyle.System;
            btnDelete.Font = new Font("Segoe UI", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(817, 16);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(78, 35);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // VoiceFile
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(tbxTranscription);
            Controls.Add(btnPlay);
            Controls.Add(lblLength);
            Name = "VoiceFile";
            Size = new Size(990, 68);
            Load += VoiceFile_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPlay;
        private TextBox tbxTranscription;
        private Button btnSave;
        private Label lblLength;
        private Button btnDelete;
    }
}
