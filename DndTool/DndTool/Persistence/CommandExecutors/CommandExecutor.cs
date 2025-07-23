using DndTool.Commands;

namespace DndTool.Persistence.CommandExecutors
{
    internal class CommandExecutor : ICommandExecutor
    {
        private readonly Stack<IUndoableCommand> _undoStack = new();
        private readonly Stack<IUndoableCommand> _redoStack = new();

        public void Execute(IUndoableCommand undoableCommand)
        {
            ArgumentNullException.ThrowIfNull(undoableCommand, nameof(undoableCommand));

            undoableCommand.Execute();
            _undoStack.Push(undoableCommand);
            _redoStack.Clear();
        }

        public bool IsUndoEnabled => _undoStack.Count > 0;
        public bool IsRedoEnabled => _redoStack.Count > 0;

        public void Undo()
        {
            if (_undoStack.TryPop(out var undoableCommand))
            {
                undoableCommand.Undo();
                _redoStack.Push(undoableCommand);
            }
        }

        public void Redo()
        {
            if (_redoStack.TryPop(out var undoableCommand))
            {
                undoableCommand.Execute();
                _undoStack.Push(undoableCommand);
            }
        }
    }
}
