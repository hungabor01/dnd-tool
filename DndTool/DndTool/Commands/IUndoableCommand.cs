namespace DndTool.Commands
{
    public interface IUndoableCommand
    {
        void Execute();

        void Undo();
    }
}
