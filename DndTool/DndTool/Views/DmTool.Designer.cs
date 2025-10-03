using System.Windows.Forms;

namespace DndTool.Views
{
    partial class DmTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DmTool));
            MainMenuStrip = new MenuStrip();
            UndoToolStripMenuItem = new ToolStripMenuItem();
            RedoToolStripMenuItem = new ToolStripMenuItem();
            CampaignToolStripMenuItem = new ToolStripMenuItem();
            NewCampaignToolStripMenuItem = new ToolStripMenuItem();
            LoadCampaignToolStripMenuItem = new ToolStripMenuItem();
            SessionPanel = new Panel();
            LastAdministrationLabel = new Label();
            SessionHistoryButton = new Button();
            SessionIndexNumberLabel = new Label();
            SessionNameTextBox = new TextBox();
            SessionEndDateDateTimePicker = new DateTimePicker();
            SessionStartDateDateTimePicker = new DateTimePicker();
            CurrentDateDateTimePicker = new DateTimePicker();
            label1 = new Label();
            NewSessionButton = new Button();
            NextSessionButton = new Button();
            PreviousSessionButton = new Button();
            BodyTabControl = new TabControl();
            PlayersTabPage = new TabPage();
            tabPage2 = new TabPage();
            MainMenuStrip.SuspendLayout();
            SessionPanel.SuspendLayout();
            BodyTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // MainMenuStrip
            // 
            MainMenuStrip.BackColor = Color.White;
            MainMenuStrip.Items.AddRange(new ToolStripItem[] { UndoToolStripMenuItem, RedoToolStripMenuItem, CampaignToolStripMenuItem });
            MainMenuStrip.Location = new Point(0, 0);
            MainMenuStrip.Name = "MainMenuStrip";
            MainMenuStrip.Size = new Size(1320, 24);
            MainMenuStrip.TabIndex = 0;
            MainMenuStrip.Text = "menuStrip1";
            // 
            // UndoToolStripMenuItem
            // 
            UndoToolStripMenuItem.BackColor = Color.White;
            UndoToolStripMenuItem.Enabled = false;
            UndoToolStripMenuItem.ForeColor = Color.Black;
            UndoToolStripMenuItem.Image = (Image)resources.GetObject("UndoToolStripMenuItem.Image");
            UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            UndoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            UndoToolStripMenuItem.Size = new Size(28, 20);
            UndoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
            // 
            // RedoToolStripMenuItem
            // 
            RedoToolStripMenuItem.BackColor = Color.White;
            RedoToolStripMenuItem.Enabled = false;
            RedoToolStripMenuItem.ForeColor = Color.Black;
            RedoToolStripMenuItem.Image = (Image)resources.GetObject("RedoToolStripMenuItem.Image");
            RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
            RedoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            RedoToolStripMenuItem.Size = new Size(28, 20);
            RedoToolStripMenuItem.Click += RedoToolStripMenuItem_Click;
            // 
            // CampaignToolStripMenuItem
            // 
            CampaignToolStripMenuItem.BackColor = Color.White;
            CampaignToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewCampaignToolStripMenuItem, LoadCampaignToolStripMenuItem });
            CampaignToolStripMenuItem.ForeColor = Color.Black;
            CampaignToolStripMenuItem.Name = "CampaignToolStripMenuItem";
            CampaignToolStripMenuItem.Size = new Size(74, 20);
            CampaignToolStripMenuItem.Text = "Campaign";
            // 
            // NewCampaignToolStripMenuItem
            // 
            NewCampaignToolStripMenuItem.BackColor = Color.White;
            NewCampaignToolStripMenuItem.Name = "NewCampaignToolStripMenuItem";
            NewCampaignToolStripMenuItem.Size = new Size(158, 22);
            NewCampaignToolStripMenuItem.Text = "New Campaign";
            NewCampaignToolStripMenuItem.Click += NewCampaignToolStripMenuItem_Click;
            // 
            // LoadCampaignToolStripMenuItem
            // 
            LoadCampaignToolStripMenuItem.BackColor = Color.White;
            LoadCampaignToolStripMenuItem.Name = "LoadCampaignToolStripMenuItem";
            LoadCampaignToolStripMenuItem.Size = new Size(158, 22);
            LoadCampaignToolStripMenuItem.Text = "Load Campaign";
            LoadCampaignToolStripMenuItem.Click += LoadCampaignToolStripMenuItem_Click;
            // 
            // SessionPanel
            // 
            SessionPanel.AutoSize = true;
            SessionPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SessionPanel.Controls.Add(LastAdministrationLabel);
            SessionPanel.Controls.Add(SessionHistoryButton);
            SessionPanel.Controls.Add(SessionIndexNumberLabel);
            SessionPanel.Controls.Add(SessionNameTextBox);
            SessionPanel.Controls.Add(SessionEndDateDateTimePicker);
            SessionPanel.Controls.Add(SessionStartDateDateTimePicker);
            SessionPanel.Controls.Add(CurrentDateDateTimePicker);
            SessionPanel.Controls.Add(label1);
            SessionPanel.Controls.Add(NewSessionButton);
            SessionPanel.Controls.Add(NextSessionButton);
            SessionPanel.Controls.Add(PreviousSessionButton);
            SessionPanel.Dock = DockStyle.Top;
            SessionPanel.Location = new Point(0, 24);
            SessionPanel.Name = "SessionPanel";
            SessionPanel.Size = new Size(1320, 38);
            SessionPanel.TabIndex = 1;
            // 
            // LastAdministrationLabel
            // 
            LastAdministrationLabel.AutoSize = true;
            LastAdministrationLabel.Font = new Font("Segoe UI", 15F);
            LastAdministrationLabel.Location = new Point(1088, 8);
            LastAdministrationLabel.Name = "LastAdministrationLabel";
            LastAdministrationLabel.Size = new Size(23, 28);
            LastAdministrationLabel.TabIndex = 10;
            LastAdministrationLabel.Text = "0";
            LastAdministrationLabel.Click += LastAdministrationLabel_Click;
            // 
            // SessionHistoryButton
            // 
            SessionHistoryButton.BackgroundImage = (Image)resources.GetObject("SessionHistoryButton.BackgroundImage");
            SessionHistoryButton.BackgroundImageLayout = ImageLayout.Stretch;
            SessionHistoryButton.ForeColor = Color.Black;
            SessionHistoryButton.Location = new Point(988, 12);
            SessionHistoryButton.Name = "SessionHistoryButton";
            SessionHistoryButton.Size = new Size(23, 23);
            SessionHistoryButton.TabIndex = 9;
            SessionHistoryButton.UseVisualStyleBackColor = true;
            SessionHistoryButton.Click += SessionHistoryButton_Click;
            // 
            // SessionIndexNumberLabel
            // 
            SessionIndexNumberLabel.AutoSize = true;
            SessionIndexNumberLabel.Font = new Font("Segoe UI", 15F);
            SessionIndexNumberLabel.Location = new Point(298, 8);
            SessionIndexNumberLabel.Name = "SessionIndexNumberLabel";
            SessionIndexNumberLabel.Size = new Size(23, 28);
            SessionIndexNumberLabel.TabIndex = 8;
            SessionIndexNumberLabel.Text = "0";
            // 
            // SessionNameTextBox
            // 
            SessionNameTextBox.Location = new Point(337, 12);
            SessionNameTextBox.Name = "SessionNameTextBox";
            SessionNameTextBox.Size = new Size(386, 23);
            SessionNameTextBox.TabIndex = 7;
            SessionNameTextBox.KeyDown += SessionNameTextBox_KeyDown;
            SessionNameTextBox.Leave += SessionNameTextBox_Leave;
            // 
            // SessionEndDateDateTimePicker
            // 
            SessionEndDateDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm dddd";
            SessionEndDateDateTimePicker.Format = DateTimePickerFormat.Custom;
            SessionEndDateDateTimePicker.Location = new Point(743, 12);
            SessionEndDateDateTimePicker.Name = "SessionEndDateDateTimePicker";
            SessionEndDateDateTimePicker.Size = new Size(180, 23);
            SessionEndDateDateTimePicker.TabIndex = 6;
            SessionEndDateDateTimePicker.ValueChanged += SessionEndDateDateTimePicker_ValueChanged;
            // 
            // SessionStartDateDateTimePicker
            // 
            SessionStartDateDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm dddd";
            SessionStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;
            SessionStartDateDateTimePicker.Location = new Point(42, 12);
            SessionStartDateDateTimePicker.Name = "SessionStartDateDateTimePicker";
            SessionStartDateDateTimePicker.Size = new Size(180, 23);
            SessionStartDateDateTimePicker.TabIndex = 5;
            SessionStartDateDateTimePicker.ValueChanged += SessionStartDateDateTimePicker_ValueChanged;
            // 
            // CurrentDateDateTimePicker
            // 
            CurrentDateDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm dddd";
            CurrentDateDateTimePicker.Format = DateTimePickerFormat.Custom;
            CurrentDateDateTimePicker.Location = new Point(1127, 12);
            CurrentDateDateTimePicker.Name = "CurrentDateDateTimePicker";
            CurrentDateDateTimePicker.Size = new Size(180, 23);
            CurrentDateDateTimePicker.TabIndex = 4;
            CurrentDateDateTimePicker.ValueChanged += CurrentDateDateTimePicker_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(226, 8);
            label1.Name = "label1";
            label1.Size = new Size(77, 28);
            label1.TabIndex = 3;
            label1.Text = "Session";
            // 
            // NewSessionButton
            // 
            NewSessionButton.BackgroundImage = (Image)resources.GetObject("NewSessionButton.BackgroundImage");
            NewSessionButton.BackgroundImageLayout = ImageLayout.Stretch;
            NewSessionButton.ForeColor = Color.Black;
            NewSessionButton.Location = new Point(959, 12);
            NewSessionButton.Name = "NewSessionButton";
            NewSessionButton.Size = new Size(23, 23);
            NewSessionButton.TabIndex = 2;
            NewSessionButton.UseVisualStyleBackColor = true;
            NewSessionButton.Click += NewSessionButton_Click;
            // 
            // NextSessionButton
            // 
            NextSessionButton.BackgroundImage = (Image)resources.GetObject("NextSessionButton.BackgroundImage");
            NextSessionButton.BackgroundImageLayout = ImageLayout.Stretch;
            NextSessionButton.Location = new Point(929, 12);
            NextSessionButton.Name = "NextSessionButton";
            NextSessionButton.Size = new Size(23, 23);
            NextSessionButton.TabIndex = 1;
            NextSessionButton.UseVisualStyleBackColor = true;
            NextSessionButton.Click += NextSessionButton_Click;
            // 
            // PreviousSessionButton
            // 
            PreviousSessionButton.BackgroundImage = (Image)resources.GetObject("PreviousSessionButton.BackgroundImage");
            PreviousSessionButton.BackgroundImageLayout = ImageLayout.Stretch;
            PreviousSessionButton.Location = new Point(11, 12);
            PreviousSessionButton.Name = "PreviousSessionButton";
            PreviousSessionButton.Size = new Size(23, 23);
            PreviousSessionButton.TabIndex = 0;
            PreviousSessionButton.UseVisualStyleBackColor = true;
            PreviousSessionButton.Click += PreviousSessionButton_Click;
            // 
            // BodyTabControl
            // 
            BodyTabControl.Controls.Add(PlayersTabPage);
            BodyTabControl.Controls.Add(tabPage2);
            BodyTabControl.Dock = DockStyle.Fill;
            BodyTabControl.Location = new Point(0, 62);
            BodyTabControl.Name = "BodyTabControl";
            BodyTabControl.SelectedIndex = 0;
            BodyTabControl.Size = new Size(1320, 667);
            BodyTabControl.TabIndex = 2;
            BodyTabControl.Resize += BodyTabControl_Resize;
            // 
            // PlayersTabPage
            // 
            PlayersTabPage.BackColor = Color.FromArgb(38, 40, 42);
            PlayersTabPage.Location = new Point(4, 24);
            PlayersTabPage.Name = "PlayersTabPage";
            PlayersTabPage.Padding = new Padding(3);
            PlayersTabPage.Size = new Size(1312, 639);
            PlayersTabPage.TabIndex = 0;
            PlayersTabPage.Text = "Players";
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1312, 639);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // DmTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 40, 42);
            ClientSize = new Size(1320, 729);
            Controls.Add(BodyTabControl);
            Controls.Add(SessionPanel);
            Controls.Add(MainMenuStrip);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1336, 768);
            Name = "DmTool";
            Text = "DmTool";
            MainMenuStrip.ResumeLayout(false);
            MainMenuStrip.PerformLayout();
            SessionPanel.ResumeLayout(false);
            SessionPanel.PerformLayout();
            BodyTabControl.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem CampaignToolStripMenuItem;
        private ToolStripMenuItem NewCampaignToolStripMenuItem;
        private ToolStripMenuItem LoadCampaignToolStripMenuItem;
        private ToolStripMenuItem UndoToolStripMenuItem;
        private ToolStripMenuItem RedoToolStripMenuItem;
        private Panel SessionPanel;
        private Label label1;
        private Button NewSessionButton;
        private Button NextSessionButton;
        private Button PreviousSessionButton;
        private DateTimePicker CurrentDateDateTimePicker;
        private TextBox SessionNameTextBox;
        private DateTimePicker SessionEndDateDateTimePicker;
        private DateTimePicker SessionStartDateDateTimePicker;
        private Label SessionIndexNumberLabel;
        private Button SessionHistoryButton;
        private Label LastAdministrationLabel;
        private TabControl BodyTabControl;
        private TabPage PlayersTabPage;
        private TabPage tabPage2;
    }
}