using DndTool.Models;

namespace DndTool.Commands.SessionCommands
{
    public class ChangeSessionNameCommand : IUndoableCommand
    {
        private readonly Session _session;
        private readonly string _newSessionName;
        private string? _oldSessionName;

        public ChangeSessionNameCommand(Session session, string newSessionName)
        {
            ArgumentNullException.ThrowIfNull(session, nameof(session));
            ArgumentException.ThrowIfNullOrWhiteSpace(newSessionName, nameof(newSessionName));

            _session = session;
            _newSessionName = newSessionName;
        }

        public void Execute()
        {
            _oldSessionName = _session.Name;
            _session.Name = _newSessionName;
        }

        public void Undo()
        {
            _session.Name = _oldSessionName!;
        }
    }
}
