using DndTool.Models;

namespace DndTool.Commands.SessionCommands
{
    public class ChangeSessionStartDateCommand : IUndoableCommand
    {
        private readonly Session _session;
        private readonly DateTime _newDateTime;
        private DateTime? _oldStartDateTime;

        public ChangeSessionStartDateCommand(Session session, DateTime newDateTime)
        {
            ArgumentNullException.ThrowIfNull(session, nameof(session));

            _session = session;
            _newDateTime = newDateTime;
        }

        public void Execute()
        {
            _oldStartDateTime = _session.StartDate;
            _session.StartDate = _newDateTime;
        }

        public void Undo()
        {
            _session.StartDate = _oldStartDateTime!.Value;
        }
    }
}
