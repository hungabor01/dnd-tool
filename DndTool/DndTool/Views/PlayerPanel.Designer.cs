namespace DndTool.Views
{
    partial class PlayerPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerPanel));
            PlayerNameLabel = new Label();
            PropertiesTableLayoutPanel = new TableLayoutPanel();
            RemovePlayerButton = new Button();
            HeaderTableLayoutPanel = new TableLayoutPanel();
            HeaderTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // PlayerNameLabel
            // 
            PlayerNameLabel.BackColor = Color.Transparent;
            PlayerNameLabel.Font = new Font("Segoe UI", 15F);
            PlayerNameLabel.ForeColor = Color.White;
            PlayerNameLabel.Location = new Point(35, 0);
            PlayerNameLabel.Name = "PlayerNameLabel";
            PlayerNameLabel.Size = new Size(356, 48);
            PlayerNameLabel.TabIndex = 4;
            PlayerNameLabel.Text = "Name";
            PlayerNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PropertiesTableLayoutPanel
            // 
            PropertiesTableLayoutPanel.AutoScroll = true;
            PropertiesTableLayoutPanel.ColumnCount = 2;
            PropertiesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PropertiesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PropertiesTableLayoutPanel.Dock = DockStyle.Fill;
            PropertiesTableLayoutPanel.Location = new Point(0, 50);
            PropertiesTableLayoutPanel.Name = "PropertiesTableLayoutPanel";
            PropertiesTableLayoutPanel.RowCount = 1;
            PropertiesTableLayoutPanel.RowStyles.Add(new RowStyle());
            PropertiesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            PropertiesTableLayoutPanel.Size = new Size(426, 307);
            PropertiesTableLayoutPanel.TabIndex = 5;
            // 
            // RemovePlayerButton
            // 
            RemovePlayerButton.BackColor = Color.Transparent;
            RemovePlayerButton.BackgroundImage = (Image)resources.GetObject("RemovePlayerButton.BackgroundImage");
            RemovePlayerButton.BackgroundImageLayout = ImageLayout.Stretch;
            RemovePlayerButton.FlatAppearance.BorderSize = 0;
            RemovePlayerButton.FlatAppearance.MouseDownBackColor = Color.Red;
            RemovePlayerButton.FlatAppearance.MouseOverBackColor = Color.Red;
            RemovePlayerButton.FlatStyle = FlatStyle.Flat;
            RemovePlayerButton.Location = new Point(394, 0);
            RemovePlayerButton.Margin = new Padding(0);
            RemovePlayerButton.Name = "RemovePlayerButton";
            RemovePlayerButton.Size = new Size(32, 32);
            RemovePlayerButton.TabIndex = 6;
            RemovePlayerButton.UseVisualStyleBackColor = false;
            RemovePlayerButton.Click += RemovePlayerButton_Click;
            // 
            // HeaderTableLayoutPanel
            // 
            HeaderTableLayoutPanel.ColumnCount = 3;
            HeaderTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            HeaderTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            HeaderTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            HeaderTableLayoutPanel.Controls.Add(RemovePlayerButton, 3, 0);
            HeaderTableLayoutPanel.Controls.Add(PlayerNameLabel, 1, 0);
            HeaderTableLayoutPanel.Dock = DockStyle.Top;
            HeaderTableLayoutPanel.Location = new Point(0, 0);
            HeaderTableLayoutPanel.Name = "HeaderTableLayoutPanel";
            HeaderTableLayoutPanel.RowCount = 1;
            HeaderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            HeaderTableLayoutPanel.Size = new Size(426, 50);
            HeaderTableLayoutPanel.TabIndex = 7;
            // 
            // PlayerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(38, 40, 42);
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(PropertiesTableLayoutPanel);
            Controls.Add(HeaderTableLayoutPanel);
            Name = "PlayerPanel";
            Size = new Size(426, 357);
            HeaderTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label PlayerNameLabel;
        private TableLayoutPanel PropertiesTableLayoutPanel;
        private Button RemovePlayerButton;
        private TableLayoutPanel HeaderTableLayoutPanel;
    }
}
