namespace VoiceDatasetEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            toolstripFile = new ToolStripDropDownButton();
            menuLoadDataset = new ToolStripMenuItem();
            menuSaveDataset = new ToolStripMenuItem();
            toolstripEdit = new ToolStripDropDownButton();
            menuFindAndReplace = new ToolStripMenuItem();
            toolstripView = new ToolStripDropDownButton();
            menuRefresh = new ToolStripMenuItem();
            menuUnload = new ToolStripMenuItem();
            toolstripLanguage = new ToolStripDropDownButton();
            menuEnglish = new ToolStripMenuItem();
            menuJapanese = new ToolStripMenuItem();
            toolstripSettings = new ToolStripButton();
            flowAudioPanel = new FlowLayoutPanel();
            btnNextPage = new Button();
            btnPreviousPage = new Button();
            lblPage = new Label();
            btnSave = new Button();
            lblLoaded = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = SystemColors.Control;
            toolStrip1.Font = new Font("Segoe UI", 11.7818184F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(18, 18);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolstripFile, toolstripEdit, toolstripView, toolstripLanguage, toolstripSettings });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1028, 32);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolstripFile
            // 
            toolstripFile.AutoToolTip = false;
            toolstripFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolstripFile.DropDownItems.AddRange(new ToolStripItem[] { menuLoadDataset, menuSaveDataset });
            toolstripFile.Image = (Image)resources.GetObject("toolstripFile.Image");
            toolstripFile.ImageTransparentColor = Color.Magenta;
            toolstripFile.Name = "toolstripFile";
            toolstripFile.Size = new Size(52, 29);
            toolstripFile.Text = "File";
            // 
            // menuLoadDataset
            // 
            menuLoadDataset.Name = "menuLoadDataset";
            menuLoadDataset.Size = new Size(194, 30);
            menuLoadDataset.Text = "Load dataset";
            menuLoadDataset.Click += menuLoadDataset_Click_1;
            // 
            // menuSaveDataset
            // 
            menuSaveDataset.Name = "menuSaveDataset";
            menuSaveDataset.Size = new Size(194, 30);
            menuSaveDataset.Text = "Save dataset";
            menuSaveDataset.Click += menuSaveDataset_Click;
            // 
            // toolstripEdit
            // 
            toolstripEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolstripEdit.DropDownItems.AddRange(new ToolStripItem[] { menuFindAndReplace });
            toolstripEdit.Image = (Image)resources.GetObject("toolstripEdit.Image");
            toolstripEdit.ImageTransparentColor = Color.Magenta;
            toolstripEdit.Name = "toolstripEdit";
            toolstripEdit.Size = new Size(56, 29);
            toolstripEdit.Text = "Edit";
            toolstripEdit.ToolTipText = "Edit";
            // 
            // menuFindAndReplace
            // 
            menuFindAndReplace.Name = "menuFindAndReplace";
            menuFindAndReplace.Size = new Size(222, 30);
            menuFindAndReplace.Text = "Find and replace";
            menuFindAndReplace.Click += menuFindAndReplace_Click;
            // 
            // toolstripView
            // 
            toolstripView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolstripView.DropDownItems.AddRange(new ToolStripItem[] { menuRefresh, menuUnload });
            toolstripView.Image = (Image)resources.GetObject("toolstripView.Image");
            toolstripView.ImageTransparentColor = Color.Magenta;
            toolstripView.Name = "toolstripView";
            toolstripView.Size = new Size(63, 29);
            toolstripView.Text = "View";
            // 
            // menuRefresh
            // 
            menuRefresh.Name = "menuRefresh";
            menuRefresh.Size = new Size(212, 30);
            menuRefresh.Text = "Refresh";
            menuRefresh.Click += menuRefresh_Click;
            // 
            // menuUnload
            // 
            menuUnload.Name = "menuUnload";
            menuUnload.Size = new Size(212, 30);
            menuUnload.Text = "Unload dataset";
            menuUnload.Click += menuUnload_Click;
            // 
            // toolstripLanguage
            // 
            toolstripLanguage.AutoToolTip = false;
            toolstripLanguage.DropDownItems.AddRange(new ToolStripItem[] { menuEnglish, menuJapanese });
            toolstripLanguage.ImageTransparentColor = Color.Magenta;
            toolstripLanguage.Name = "toolstripLanguage";
            toolstripLanguage.Size = new Size(103, 29);
            toolstripLanguage.Text = "Language";
            // 
            // menuEnglish
            // 
            menuEnglish.Name = "menuEnglish";
            menuEnglish.Size = new Size(149, 30);
            menuEnglish.Text = "English";
            menuEnglish.Click += englishToolStripMenuItem_Click;
            // 
            // menuJapanese
            // 
            menuJapanese.Name = "menuJapanese";
            menuJapanese.Size = new Size(149, 30);
            menuJapanese.Text = "日本語";
            menuJapanese.Click += 日本語ToolStripMenuItem_Click;
            // 
            // toolstripSettings
            // 
            toolstripSettings.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolstripSettings.Image = (Image)resources.GetObject("toolstripSettings.Image");
            toolstripSettings.ImageTransparentColor = Color.Magenta;
            toolstripSettings.Name = "toolstripSettings";
            toolstripSettings.Size = new Size(80, 29);
            toolstripSettings.Text = "Settings";
            toolstripSettings.Click += menuSettings_Click;
            // 
            // flowAudioPanel
            // 
            flowAudioPanel.AllowDrop = true;
            flowAudioPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowAudioPanel.AutoScroll = true;
            flowAudioPanel.BackColor = SystemColors.ControlLight;
            flowAudioPanel.Location = new Point(12, 78);
            flowAudioPanel.Name = "flowAudioPanel";
            flowAudioPanel.Size = new Size(1004, 765);
            flowAudioPanel.TabIndex = 0;
            // 
            // btnNextPage
            // 
            btnNextPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNextPage.Font = new Font("Segoe UI", 11.7818184F);
            btnNextPage.Location = new Point(925, 39);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(91, 33);
            btnNextPage.TabIndex = 2;
            btnNextPage.Text = "Next";
            btnNextPage.UseVisualStyleBackColor = true;
            btnNextPage.Click += btnNextPage_Click;
            // 
            // btnPreviousPage
            // 
            btnPreviousPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPreviousPage.Font = new Font("Segoe UI", 11.7818184F);
            btnPreviousPage.Location = new Point(828, 39);
            btnPreviousPage.Name = "btnPreviousPage";
            btnPreviousPage.Size = new Size(91, 33);
            btnPreviousPage.TabIndex = 3;
            btnPreviousPage.Text = "Previous";
            btnPreviousPage.UseVisualStyleBackColor = true;
            btnPreviousPage.Click += btnPreviousPage_Click;
            // 
            // lblPage
            // 
            lblPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPage.Font = new Font("Segoe UI", 11.7818184F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPage.Location = new Point(662, 39);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(165, 33);
            lblPage.TabIndex = 4;
            lblPage.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 11.7818184F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(12, 41);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(82, 33);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblLoaded
            // 
            lblLoaded.AutoSize = true;
            lblLoaded.Location = new Point(100, 48);
            lblLoaded.Name = "lblLoaded";
            lblLoaded.Size = new Size(0, 19);
            lblLoaded.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1028, 855);
            Controls.Add(lblLoaded);
            Controls.Add(btnSave);
            Controls.Add(lblPage);
            Controls.Add(btnPreviousPage);
            Controls.Add(btnNextPage);
            Controls.Add(flowAudioPanel);
            Controls.Add(toolStrip1);
            MinimumSize = new Size(1046, 300);
            Name = "Form1";
            Text = "Voice Dataset Editor";
            Shown += Form1_Shown;
            Resize += Form1_Resize;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolstripEdit;
        private FlowLayoutPanel flowAudioPanel;
        private Button btnNextPage;
        private Button btnPreviousPage;
        private Label lblPage;
        private ToolStripDropDownButton toolstripLanguage;
        private Button btnSave;
        private ToolStripMenuItem menuFindAndReplace;
        private ToolStripDropDownButton toolstripFile;
        private ToolStripMenuItem menuLoadDataset;
        private ToolStripMenuItem menuSaveDataset;
        private Label lblLoaded;
        private ToolStripMenuItem menuEnglish;
        private ToolStripMenuItem menuJapanese;
        private ToolStripDropDownButton toolstripView;
        private ToolStripMenuItem menuRefresh;
        private ToolStripButton toolstripSettings;
        private ToolStripMenuItem menuUnload;
    }
}
