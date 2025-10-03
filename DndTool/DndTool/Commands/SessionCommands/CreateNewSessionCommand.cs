using DndTool.Models;
using DndTool.Queries;

namespace DndTool.Commands.SessionCommands
{
    public class CreateNewSessionCommand : IUndoableCommand
    {
        private readonly Sessions _sessions;
        private readonly string _sessionName;
        private int _oldCurrentSessionIndexNumber;

        public CreateNewSessionCommand(Sessions sessions, string sessionName)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));
            ArgumentException.ThrowIfNullOrWhiteSpace(sessionName, nameof(sessionName));

            _sessions = sessions;
            _sessionName = sessionName;
        }

        public void Execute()
        {
            _oldCurrentSessionIndexNumber = _sessions.CurrentSessionIndex;

            var session = new Session(++_sessions.CurrentSessionIndex, _sessionName);
            _sessions.SessionList.Add(session);
        }

        public void Undo()
        {
            var sessionQueries = new SessionQueries();
            var lastSession = sessionQueries.GetLastSession(_sessions)!;
            _sessions.SessionList.Remove(lastSession);
            _sessions.CurrentSessionIndex = _oldCurrentSessionIndexNumber;
        }
    }
}
