using DndTool.Common;
using DndTool.Queries;
using DndTool.ViewModels;
using System.Diagnostics;
using DndTool.Models;
using DndTool.Views.Controls;
using DndTool.Views.Events;

namespace DndTool.Views
{
    public partial class DmTool : Form
    {
        private const int MaxPlayerPanelColumns = 4;
        private const int MaxPlayerPanelRows = 2;
        private const int MaxPlayer = MaxPlayerPanelColumns * MaxPlayerPanelRows;

        private readonly IDmToolViewModel _viewModel;
        private bool _suppressValueChangeEvent;

        private List<Panel> _playerPanels;
        private Button _addPlayerButton;

        #region Constructor

        public DmTool()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetEnableOfAllControls(false);

            _viewModel = new DmToolViewModel();
            _viewModel.CommandExecuted += SetUndoRedoButtons;
        }

        private void InitializeCustomComponents()
        {
            CreateCustomComponents();

            SuspendLayoutForAll();

            var size = GetPlayerPanelSize();
            for (var i = 0; i < MaxPlayer; i++)
            {
                InitializePlayerPanel(i, size);
                PlayersTabPage.Controls.Add(_playerPanels[i]);
            }

            InitializeAddPlayerButton(size);
            _playerPanels[0].Controls.Add(_addPlayerButton);

            ResumeLayoutForAll();
        }

        private void SuspendLayoutForAll()
        {
            PlayersTabPage.SuspendLayout();
            _playerPanels.ForEach(panel => panel.SuspendLayout());
        }

        private void ResumeLayoutForAll()
        {
            _playerPanels.ForEach(panel => panel.ResumeLayout(false));
            PlayersTabPage.ResumeLayout(false);
        }

        private void CreateCustomComponents()
        {
            _playerPanels = new List<Panel>();
            for (var i = 0; i < MaxPlayer; i++)
            {
                _playerPanels.Add(new Panel());
            }

            _addPlayerButton = new Button();
        }

        private void InitializePlayerPanel(int i, Size size)
        {
            var panel = _playerPanels[i];
            panel.Name = "PlayerPanel" + i;
            panel.Size = size;
            panel.Location = GetPlayerPanelLocation(i, size.Width, size.Height);
            panel.TabIndex = i;
        }

        private void InitializeAddPlayerButton(Size size)
        {
            _addPlayerButton.BackgroundImageLayout = ImageLayout.None;
            _addPlayerButton.FlatAppearance.BorderSize = 0;
            _addPlayerButton.FlatStyle = FlatStyle.Flat;
            _addPlayerButton.Font = new Font("Arial", 48F);
            _addPlayerButton.ForeColor = Color.White;
            _addPlayerButton.Location = new Point(0, 0);
            _addPlayerButton.Name = "AddPlayerButton";
            _addPlayerButton.Size = size;
            _addPlayerButton.Text = "+";
            _addPlayerButton.Click += AddPlayerButton_Click;
        }

        private void SetUndoRedoButtons(object? sender, EventArgs eventArgs)
        {
            UndoToolStripMenuItem.Enabled = _viewModel.IsUndoEnabled;
            RedoToolStripMenuItem.Enabled = _viewModel.IsRedoEnabled;
        }

        #endregion

        #region Populate Methods

        private void PopulateCampaignData()
        {
            var campaign = _viewModel.Campaign;
            Text = $@"DM Tool - {Path.GetFileNameWithoutExtension(campaign.Name)}";

            PopulateCurrentDateTimePicker();
            PopulateLastAdministrationLabel();
            PopulateSessionData();
            PopulatePlayerDatas();
        }

        private void PopulateCurrentDateTimePicker()
        {
            _suppressValueChangeEvent = true;

            var campaign = _viewModel.Campaign;
            CurrentDateDateTimePicker.Value = campaign.CurrentDate;

            _suppressValueChangeEvent = false;
        }

        private void PopulateLastAdministrationLabel()
        {
            _suppressValueChangeEvent = true;

            var campaign = _viewModel.Campaign;
            var days = (campaign.CurrentDate - campaign.LastAdministrationDate).Days;
            LastAdministrationLabel.Text = days.ToString("N0");
            LastAdministrationLabel.ForeColor = days > 0 ? Color.Red : Color.White;

            _suppressValueChangeEvent = false;
        }

        private void PopulateSessionData()
        {
            _suppressValueChangeEvent = true;

            var sessions = _viewModel.Campaign.Sessions;
            var currentSession = GetCurrentSession();
            if (!sessions.SessionList.Any() || currentSession == null)
            {
                SessionNameTextBox.Enabled = false;
                PreviousSessionButton.Enabled = false;
                NextSessionButton.Enabled = false;
                SessionStartDateDateTimePicker.Enabled = false;
                SessionEndDateDateTimePicker.Enabled = false;
                SessionHistoryButton.Enabled = false;

                SessionIndexNumberLabel.Text = "0";
                SessionNameTextBox.Text = string.Empty;
            }
            else
            {
                var sessionQueries = new SessionQueries();
                SessionNameTextBox.Enabled = true;
                PreviousSessionButton.Enabled = sessionQueries.HasPreviousSession(sessions);
                NextSessionButton.Enabled = sessionQueries.HasNextSession(sessions);
                SessionStartDateDateTimePicker.Enabled = true;
                SessionEndDateDateTimePicker.Enabled = true;
                SessionHistoryButton.Enabled = true;

                SessionIndexNumberLabel.Text = currentSession.IndexNumber.ToString();
                SessionNameTextBox.Text = currentSession.Name;
                SessionStartDateDateTimePicker.Value = currentSession.StartDate;
                SessionEndDateDateTimePicker.Value = currentSession.EndDate;
            }

            _suppressValueChangeEvent = false;
        }

        private void PopulatePlayerDatas()
        {
            _playerPanels.ForEach(panel => panel.Controls.Clear());

            var campaign = _viewModel.Campaign;
            var playerCount = Math.Min(campaign.Players.Count, MaxPlayer);
            for (var i = 0; i < playerCount; i++)
            {
                var panel = _playerPanels[i];
                var player = campaign.Players[i];
                PopulatePlayerData(panel, player);
            }

            if (playerCount < MaxPlayer)
            {
                _playerPanels[playerCount].Controls.Add(_addPlayerButton);
            }
        }

        private void PopulatePlayerData(Panel parent, Player player)
        {
            _suppressValueChangeEvent = true;

            var playerPanel = new PlayerPanel(player)
            {
                Name = player.Id.ToString(),
                Dock = DockStyle.Fill,
            };
            playerPanel.OnPlayerRemoved += OnPlayerRemoved;

            var propertiesTableLayoutPanel = playerPanel.Controls.Find("PropertiesTableLayoutPanel", true).First() as TableLayoutPanel;
            propertiesTableLayoutPanel.RowCount = 0;
            propertiesTableLayoutPanel.RowStyles.Clear();

            for (var i = 0; i < player.Properties.Count; i++)
            {
                propertiesTableLayoutPanel.RowCount += 1;
                propertiesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                propertiesTableLayoutPanel.Controls
                    .Add(CreateTextBox(player, i, true), 0, i);
                propertiesTableLayoutPanel.Controls
                    .Add(CreateTextBox(player, i, false), 1, i);
            }

            propertiesTableLayoutPanel.RowCount += 1;
            propertiesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            propertiesTableLayoutPanel.Controls
                .Add(CreateTextBox(player, player.Properties.Count, true), 0, propertiesTableLayoutPanel.RowCount);
            propertiesTableLayoutPanel.Controls
                .Add(CreateTextBox(player, player.Properties.Count, false, false), 1, propertiesTableLayoutPanel.RowCount);

            parent.Controls.Clear();
            parent.Controls.Add(playerPanel);

            _suppressValueChangeEvent = false;
        }

        private PlayerPropertyTextBox CreateTextBox(Player player, int propertyIndex, bool isPropertyName, bool isEnabled = true)
        {
            var text = propertyIndex >= player.Properties.Count
                ? string.Empty
                : isPropertyName
                    ? player.Properties[propertyIndex].PropertyName
                    : player.Properties[propertyIndex].PropertyValue;

            var textBox = new PlayerPropertyTextBox(player, propertyIndex, isPropertyName);
            textBox.Dock = DockStyle.Fill;
            textBox.Text = text;
            textBox.Enabled = isEnabled;
            textBox.Leave += PropertyTextBox_Leave;
            textBox.KeyDown += PropertyTextBox_OnKeyDown;
            return textBox;
        }

        #endregion

        #region MenuStrip Event Handlers

        private async void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _viewModel.Undo();

            PopulateCampaignData();
        }

        private async void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _viewModel.Redo();

            PopulateCampaignData();
        }

        private async void NewCampaignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialog("Please enter a campaign name!");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await _viewModel.CreateNewCampaign(dialog.Value);

                PopulateCampaignData();
                SetEnableOfAllControls(true);
            }
        }

        private async void LoadCampaignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Constants.Persistence.SaveFilesPath,
                Filter = @"Campaign Files (*.campaign)|*.campaign",
                Title = @"Open Campaign File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFile = openFileDialog.FileName;
                await _viewModel.LoadCampaign(Path.GetFileNameWithoutExtension(selectedFile));

                PopulateCampaignData();
                SetEnableOfAllControls(true);
            }
        }

        #endregion

        #region Session Panel Event Handlers

        private async void CurrentDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressValueChangeEvent)
            {
                return;
            }

            await _viewModel.ChangeCurrentDate(CurrentDateDateTimePicker.Value);

            PopulateLastAdministrationLabel();
        }

        private async void LastAdministrationLabel_Click(object sender, EventArgs e)
        {
            await _viewModel.ChangeLastAdministrationDate();

            PopulateLastAdministrationLabel();
        }

        private async void NewSessionButton_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialog("Please enter a session name!");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await _viewModel.CreateNewSession(dialog.Value);

                PopulateSessionData();
            }
        }

        private async void SessionNameTextBox_Leave(object sender, EventArgs e)
        {
            if (_suppressValueChangeEvent)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(SessionNameTextBox.Text))
            {
                var currentSession = GetCurrentSession()!;
                SessionNameTextBox.Text = currentSession.Name;
                return;
            }

            await _viewModel.ChangeSessionName(SessionNameTextBox.Text);
        }

        private void SessionNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = null;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private async void SessionStartDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressValueChangeEvent)
            {
                return;
            }

            await _viewModel.ChangeSessionStartDate(SessionStartDateDateTimePicker.Value);
        }

        private async void SessionEndDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressValueChangeEvent)
            {
                return;
            }

            await _viewModel.ChangeSessionEndDate(SessionEndDateDateTimePicker.Value);
        }

        private async void PreviousSessionButton_Click(object sender, EventArgs e)
        {
            await _viewModel.DecrementCurrentSession();

            PopulateSessionData();
        }

        private async void NextSessionButton_Click(object sender, EventArgs e)
        {
            await _viewModel.IncrementCurrentSession();

            PopulateSessionData();
        }

        private void SessionHistoryButton_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open Text File";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Path.GetDirectoryName(_viewModel.Campaign.SessionHistoryFolder);
            openFileDialog.FileName = Path.GetFileName(_viewModel.Campaign.SessionHistoryFolder);
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                _viewModel.SaveSessionHistoryFolder(filePath);
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        #endregion

        #region BodyTabControl

        private void BodyTabControl_Resize(object sender, EventArgs e)
        {
            var size = GetPlayerPanelSize();
            for (var i = 0; i < _playerPanels.Count; i++)
            {
                var panel = _playerPanels[i];
                panel.Size = size;
                panel.Location = GetPlayerPanelLocation(i, size.Width, size.Height);
            }

            _addPlayerButton.Width = size.Width;
            _addPlayerButton.Height = size.Height;
        }

        private async void AddPlayerButton_Click(object? sender, EventArgs e)
        {
            if (_addPlayerButton.Parent is not Panel parent)
            {
                return;
            }

            var dialog = new InputDialog("Please enter a player name!");
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var player = await _viewModel.CreateNewPlayer(dialog.Value);

            PopulatePlayerData(parent, player);
            ShiftAddPlayerButton(parent);
        }

        private void ShiftAddPlayerButton(Panel parent)
        {
            parent.Controls.Remove(_addPlayerButton);

            var newIndex = _playerPanels.IndexOf(parent) + 1;
            if (newIndex < MaxPlayer)
            {
                _playerPanels[newIndex].Controls.Add(_addPlayerButton);
            }
        }

        private async void PropertyTextBox_Leave(object? sender, EventArgs e)
        {
            if (_suppressValueChangeEvent)
            {
                return;
            }

            if (sender is not PlayerPropertyTextBox textBox)
            {
                return;
            }

            var hasPlayerPropertyChanged = await _viewModel.ChangePlayerProperty(textBox.Player, textBox.PropertyIndex, textBox.IsPropertyName, textBox.Text);
            if (hasPlayerPropertyChanged)
            {
                var panel = textBox.Parent?.Parent?.Parent as Panel;
                PopulatePlayerData(panel!, textBox.Player);
            }
        }

        private void PropertyTextBox_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = null;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private async void OnPlayerRemoved(object? sender, PlayerRemovedEventArgs e)
        {
            if (sender is not PlayerPanel playerPanel)
            {
                return;
            }

            playerPanel.OnPlayerRemoved -= OnPlayerRemoved;

            await _viewModel.RemovePlayer(e.RemovedPlayer);

            PopulatePlayerDatas();
        }

        #endregion

        #region Helper Methods

        private void SetEnableOfAllControls(bool isEnabled)
        {
            foreach (Control control in Controls)
            {
                if (control is MenuStrip)
                {
                    continue;
                }

                control.Enabled = isEnabled;
            }
        }

        private Session? GetCurrentSession()
        {
            var sessionQueries = new SessionQueries();
            return sessionQueries.GetCurrentSession(_viewModel.Campaign.Sessions);
        }

        private Size GetPlayerPanelSize()
        {
            var width = Convert.ToInt32(PlayersTabPage.Width / MaxPlayerPanelColumns);
            var height = Convert.ToInt32(PlayersTabPage.Height / MaxPlayerPanelRows);
            return new Size(width, height);
        }

        private Point GetPlayerPanelLocation(int index, int width, int height)
        {
            var x = Convert.ToInt32(index % MaxPlayerPanelColumns * width);
            var y = Convert.ToInt32(index / MaxPlayerPanelColumns * height);
            return new Point(x, y);
        }

        #endregion
    }
}
