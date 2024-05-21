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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
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
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(124, 8);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Replace this";
            textBox1.Size = new Size(397, 27);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(124, 52);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "With this";
            textBox2.Size = new Size(397, 26);
            textBox2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.1272726F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(8, 9);
            label1.Name = "label1";
            label1.Size = new Size(88, 23);
            label1.TabIndex = 3;
            label1.Text = "Find what:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.1272726F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(8, 52);
            label2.Name = "label2";
            label2.Size = new Size(110, 23);
            label2.TabIndex = 4;
            label2.Text = "Replace with:";
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.Location = new Point(352, 95);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(86, 27);
            btnReplaceAll.TabIndex = 5;
            btnReplaceAll.Text = "Replace all";
            btnReplaceAll.UseVisualStyleBackColor = true;
            // 
            // btnReplacePage
            // 
            btnReplacePage.Location = new Point(249, 95);
            btnReplacePage.Name = "btnReplacePage";
            btnReplacePage.Size = new Size(97, 27);
            btnReplacePage.TabIndex = 5;
            btnReplacePage.Text = "Replace page";
            btnReplacePage.UseVisualStyleBackColor = true;
            // 
            // FindAndReplace
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 134);
            Controls.Add(btnReplacePage);
            Controls.Add(btnReplaceAll);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(551, 178);
            MinimizeBox = false;
            MinimumSize = new Size(551, 178);
            Name = "FindAndReplace";
            Text = "Find and replace";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private Button btnReplaceAll;
        private Button btnReplacePage;
    }
}