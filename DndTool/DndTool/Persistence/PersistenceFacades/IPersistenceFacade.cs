using DndTool.Commands;
using DndTool.Models;

namespace DndTool.Persistence.PersistenceFacades
{
    public interface IPersistenceFacade
    {
        public Campaign Campaign { get; }
        public bool IsUndoEnabled { get; }
        public bool IsRedoEnabled { get; }
        public event EventHandler? CommandExecuted;

        public Task Execute(IUndoableCommand undoableCommand);

        public Task Undo();

        public Task Redo();

        public Task CreateNewCampaign(string name);

        public Task LoadCampaign(string name);

        public Task SaveSessionHistoryFolder(string? sessionHistoryFolder);

        public Task<Player> CreateNewPlayer(string playerName);
    }
}
