using DndTool.Common;
using DndTool.Queries;
using DndTool.ViewModels;

namespace DndTool.Views
{
    public partial class DmTool : Form
    {
        private readonly IDmToolViewModel _viewModel;
        private bool _suppressValueChangeEvent;

        public DmTool()
        {
            InitializeComponent();
            SetEnableOfAllControls(false);

            _viewModel = new DmToolViewModel();
            _viewModel.CommandExecuted += SetUndoRedoButtons;
        }

        private void SetUndoRedoButtons(object? sender, EventArgs eventArgs)
        {
            UndoToolStripMenuItem.Enabled = _viewModel.IsUndoEnabled;
            RedoToolStripMenuItem.Enabled = _viewModel.IsRedoEnabled;
        }

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

        private void PopulateCampaignData()
        {
            _suppressValueChangeEvent = true;

            var campaign = _viewModel.Campaign;
            Text = $@"DM Tool - {Path.GetFileNameWithoutExtension(campaign.Name)}";
            CurrentDateDateTimePicker.Value = campaign.CurrentDate;

            PopulateSessionData();

            _suppressValueChangeEvent = false;
        }

        private void PopulateSessionData()
        {
            _suppressValueChangeEvent = true;

            var sessionQueries = new SessionQueries();
            var sessions = _viewModel.Campaign.Sessions;
            var currentSession = sessionQueries.CurrentSession(sessions);
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
            if (_suppressValueChangeEvent)
            {
                return;
            }

            await _viewModel.DecrementCurrentSession();

            PopulateSessionData();
        }

        private async void NextSessionButton_Click(object sender, EventArgs e)
        {
            if (_suppressValueChangeEvent)
            {
                return;
            }

            await _viewModel.IncrementCurrentSession();

            PopulateSessionData();
        }

        #endregion
    }
}
