namespace VoiceDatasetEditor.Forms
{
    partial class DeleteWhere
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
            lblDeleteTranscriptionsWhere = new Label();
            cbbDeleteCondition = new ComboBox();
            tbxValue = new TextBox();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // lblDeleteTranscriptionsWhere
            // 
            lblDeleteTranscriptionsWhere.AutoSize = true;
            lblDeleteTranscriptionsWhere.Font = new Font("Segoe UI", 11.1272726F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDeleteTranscriptionsWhere.Location = new Point(12, 9);
            lblDeleteTranscriptionsWhere.Name = "lblDeleteTranscriptionsWhere";
            lblDeleteTranscriptionsWhere.Size = new Size(222, 23);
            lblDeleteTranscriptionsWhere.TabIndex = 0;
            lblDeleteTranscriptionsWhere.Text = "Delete transcriptions where:";
            // 
            // cbbDeleteCondition
            // 
            cbbDeleteCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbDeleteCondition.Font = new Font("Segoe UI", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbbDeleteCondition.FormattingEnabled = true;
            cbbDeleteCondition.Items.AddRange(new object[] { "audio length <", "audio length >", "contains text" });
            cbbDeleteCondition.Location = new Point(12, 44);
            cbbDeleteCondition.Name = "cbbDeleteCondition";
            cbbDeleteCondition.Size = new Size(138, 28);
            cbbDeleteCondition.TabIndex = 0;
            // 
            // tbxValue
            // 
            tbxValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxValue.Font = new Font("Segoe UI", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxValue.Location = new Point(156, 45);
            tbxValue.Name = "tbxValue";
            tbxValue.Size = new Size(209, 27);
            tbxValue.TabIndex = 1;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(371, 44);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(86, 28);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // DeleteWhere
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 88);
            Controls.Add(btnDelete);
            Controls.Add(tbxValue);
            Controls.Add(cbbDeleteCondition);
            Controls.Add(lblDeleteTranscriptionsWhere);
            MaximizeBox = false;
            MaximumSize = new Size(5000, 132);
            MinimizeBox = false;
            MinimumSize = new Size(339, 132);
            Name = "DeleteWhere";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "DeleteWhere";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDeleteTranscriptionsWhere;
        private ComboBox cbbDeleteCondition;
        private TextBox tbxValue;
        private Button btnDelete;
    }
}