using DndTool.Models;

namespace DndTool.ViewModels
{
    public interface IDmToolViewModel
    {
        public Campaign Campaign { get; }
        public bool IsUndoEnabled { get; }
        public bool IsRedoEnabled { get; }
        public event EventHandler? CommandExecuted;

        public Task Undo();

        public Task Redo();

        public Task CreateNewCampaign(string name);

        public Task LoadCampaign(string name);

        public Task ChangeCurrentDate(DateTime newDateTime);

        public Task CreateNewSession(string sessionName);

        public Task ChangeSessionStartDate(DateTime newDateTime);

        public Task ChangeSessionEndDate(DateTime newDateTime);

        public Task ChangeSessionName(string newSessionName);

        public Task IncrementCurrentSession();

        public Task DecrementCurrentSession();
    }
}
