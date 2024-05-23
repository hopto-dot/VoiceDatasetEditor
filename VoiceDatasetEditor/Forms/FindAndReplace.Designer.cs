namespace VoiceDatasetEditor.Forms
{
    partial class FindAndReplace
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
            btnCancel = new Button();
            tbxFind = new TextBox();
            tbxReplace = new TextBox();
            lblFindWhat = new Label();
            lblReplaceWith = new Label();
            btnReplaceAll = new Button();
            btnReplacePage = new Button();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9.163636F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(444, 95);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(77, 27);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // tbxFind
            // 
            tbxFind.Font = new Font("Segoe UI", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxFind.Location = new Point(139, 8);
            tbxFind.Name = "tbxFind";
            tbxFind.PlaceholderText = "Replace this";
            tbxFind.Size = new Size(382, 27);
            tbxFind.TabIndex = 1;
            // 
            // tbxReplace
            // 
            tbxReplace.Location = new Point(139, 52);
            tbxReplace.Name = "tbxReplace";
            tbxReplace.PlaceholderText = "With this";
            tbxReplace.Size = new Size(382, 26);
            tbxReplace.TabIndex = 2;
            // 
            // lblFindWhat
            // 
            lblFindWhat.AutoSize = true;
            lblFindWhat.Font = new Font("Segoe UI", 11.1272726F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFindWhat.Location = new Point(8, 9);
            lblFindWhat.Name = "lblFindWhat";
            lblFindWhat.Size = new Size(88, 23);
            lblFindWhat.TabIndex = 3;
            lblFindWhat.Text = "Find what:";
            // 
            // lblReplaceWith
            // 
            lblReplaceWith.AutoSize = true;
            lblReplaceWith.Font = new Font("Segoe UI", 11.1272726F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReplaceWith.Location = new Point(8, 52);
            lblReplaceWith.Name = "lblReplaceWith";
            lblReplaceWith.Size = new Size(110, 23);
            lblReplaceWith.TabIndex = 4;
            lblReplaceWith.Text = "Replace with:";
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.Location = new Point(352, 95);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(86, 27);
            btnReplaceAll.TabIndex = 5;
            btnReplaceAll.Text = "Replace all";
            btnReplaceAll.UseVisualStyleBackColor = true;
            btnReplaceAll.Click += btnReplaceAll_Click;
            // 
            // btnReplacePage
            // 
            btnReplacePage.Location = new Point(249, 95);
            btnReplacePage.Name = "btnReplacePage";
            btnReplacePage.Size = new Size(97, 27);
            btnReplacePage.TabIndex = 5;
            btnReplacePage.Text = "Replace page";
            btnReplacePage.UseVisualStyleBackColor = true;
            btnReplacePage.Visible = false;
            // 
            // FindAndReplace
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(533, 134);
            Controls.Add(tbxFind);
            Controls.Add(tbxReplace);
            Controls.Add(btnReplacePage);
            Controls.Add(btnReplaceAll);
            Controls.Add(lblReplaceWith);
            Controls.Add(lblFindWhat);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(551, 178);
            MinimizeBox = false;
            MinimumSize = new Size(551, 178);
            Name = "FindAndReplace";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Find and replace";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private TextBox tbxFind;
        private TextBox tbxReplace;
        private Label lblFindWhat;
        private Label lblReplaceWith;
        private Button btnReplaceAll;
        private Button btnReplacePage;
    }
}