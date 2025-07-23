using DndTool.Models;
using DndTool.Queries;

namespace DndTool.Commands.SessionCommands
{
    public class CreateNewSessionCommand : IUndoableCommand
    {
        private readonly Sessions _sessions;
        private readonly string _sessionName;
        private int? _oldCurrentSessionIndexNumber;

        public CreateNewSessionCommand(Sessions sessions, string sessionName)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));
            ArgumentException.ThrowIfNullOrWhiteSpace(sessionName, nameof(sessionName));

            _sessions = sessions;
            _sessionName = sessionName;
        }

        public void Execute()
        {
            var sessionQueries = new SessionQueries();
            _oldCurrentSessionIndexNumber = sessionQueries.CurrentSession(_sessions)?.IndexNumber;
            var indexNumber = sessionQueries.GetNewSessionIndex(_sessions);
            var session = new Session(indexNumber, _sessionName);
            _sessions.SessionList.Add(session);
            _sessions.CurrentSessionIndex = session.IndexNumber;
        }

        public void Undo()
        {
            var sessionQueries = new SessionQueries();
            var lastSession = sessionQueries.Last(_sessions);
            if (lastSession != null && _oldCurrentSessionIndexNumber.HasValue)
            {
                _sessions.SessionList.Remove(lastSession);
                _sessions.CurrentSessionIndex = _oldCurrentSessionIndexNumber.Value;
            }
        }
    }
}
