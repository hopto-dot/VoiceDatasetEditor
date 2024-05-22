namespace VoiceDatasetEditor.Forms
{
    partial class Settings
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
            lblItemsPerPage = new Label();
            tbxItemsPerPage = new TextBox();
            cbbLanguage = new ComboBox();
            lblLanguage = new Label();
            btnSave = new Button();
            btnClose = new Button();
            lblResizeEntries = new Label();
            cbResizeEntries = new CheckBox();
            SuspendLayout();
            // 
            // lblItemsPerPage
            // 
            lblItemsPerPage.AutoSize = true;
            lblItemsPerPage.Font = new Font("Segoe UI", 9.818182F);
            lblItemsPerPage.Location = new Point(12, 49);
            lblItemsPerPage.Name = "lblItemsPerPage";
            lblItemsPerPage.Size = new Size(109, 20);
            lblItemsPerPage.TabIndex = 0;
            lblItemsPerPage.Text = "Items per page";
            // 
            // tbxItemsPerPage
            // 
            tbxItemsPerPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbxItemsPerPage.Font = new Font("Segoe UI", 9.818182F);
            tbxItemsPerPage.Location = new Point(182, 46);
            tbxItemsPerPage.Name = "tbxItemsPerPage";
            tbxItemsPerPage.Size = new Size(139, 27);
            tbxItemsPerPage.TabIndex = 1;
            // 
            // cbbLanguage
            // 
            cbbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbbLanguage.AutoCompleteCustomSource.AddRange(new string[] { "EN", "JP" });
            cbbLanguage.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbbLanguage.DropDownHeight = 70;
            cbbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbLanguage.Font = new Font("Segoe UI", 9.818182F);
            cbbLanguage.FormattingEnabled = true;
            cbbLanguage.IntegralHeight = false;
            cbbLanguage.Items.AddRange(new object[] { "EN", "JP" });
            cbbLanguage.Location = new Point(182, 11);
            cbbLanguage.Name = "cbbLanguage";
            cbbLanguage.Size = new Size(139, 28);
            cbbLanguage.TabIndex = 2;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI", 9.818182F);
            lblLanguage.Location = new Point(12, 14);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(74, 20);
            lblLanguage.TabIndex = 3;
            lblLanguage.Text = "Language";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(238, 117);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(83, 30);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(12, 221);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(86, 26);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += button1_Click;
            // 
            // lblResizeEntries
            // 
            lblResizeEntries.AutoSize = true;
            lblResizeEntries.Font = new Font("Segoe UI", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblResizeEntries.Location = new Point(12, 84);
            lblResizeEntries.Name = "lblResizeEntries";
            lblResizeEntries.Size = new Size(99, 20);
            lblResizeEntries.TabIndex = 6;
            lblResizeEntries.Text = "Resize entries";
            // 
            // cbResizeEntries
            // 
            cbResizeEntries.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbResizeEntries.AutoSize = true;
            cbResizeEntries.Location = new Point(182, 89);
            cbResizeEntries.Name = "cbResizeEntries";
            cbResizeEntries.Size = new Size(15, 14);
            cbResizeEntries.TabIndex = 7;
            cbResizeEntries.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(333, 159);
            Controls.Add(cbResizeEntries);
            Controls.Add(lblResizeEntries);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(lblLanguage);
            Controls.Add(cbbLanguage);
            Controls.Add(tbxItemsPerPage);
            Controls.Add(lblItemsPerPage);
            MaximizeBox = false;
            MaximumSize = new Size(375, 250);
            MinimizeBox = false;
            MinimumSize = new Size(302, 175);
            Name = "Settings";
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Settings";
            TopMost = true;
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblItemsPerPage;
        private TextBox tbxItemsPerPage;
        private ComboBox cbbLanguage;
        private Label lblLanguage;
        private Button btnSave;
        private Button btnClose;
        private Label lblResizeEntries;
        private CheckBox cbResizeEntries;
    }
}