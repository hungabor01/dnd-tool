using DndTool.Commands;

namespace DndTool.Persistence.CommandExecutors
{
    internal interface ICommandExecutor
    {
        public bool IsUndoEnabled { get; }
        public bool IsRedoEnabled { get; }

        public void Execute(IUndoableCommand undoableCommand);

        public void Undo();

        public void Redo();
    }
}
