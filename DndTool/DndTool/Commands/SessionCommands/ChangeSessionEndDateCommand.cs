using DndTool.Models;

namespace DndTool.Commands.SessionCommands
{
    public class ChangeSessionEndDateCommand : IUndoableCommand
    {
        private readonly Session _session;
        private readonly DateTime _newDateTime;
        private DateTime? _oldEndDateTime;

        public ChangeSessionEndDateCommand(Session session, DateTime newDateTime)
        {
            ArgumentNullException.ThrowIfNull(session, nameof(session));

            _session = session;
            _newDateTime = newDateTime;
        }

        public void Execute()
        {
            _oldEndDateTime = _session.EndDate;
            _session.EndDate = _newDateTime;
        }

        public void Undo()
        {
            _session.EndDate = _oldEndDateTime!.Value;
        }
    }
}
