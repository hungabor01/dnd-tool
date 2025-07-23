using DndTool.Models;

namespace DndTool.Queries
{
    internal class SessionQueries
    {
        public Session? CurrentSession(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            var currentSessionIndex = sessions.CurrentSessionIndex;
            return sessions.SessionList.SingleOrDefault(session => session.IndexNumber == currentSessionIndex);
        }

        public int GetNewSessionIndex(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            return sessions.SessionList.Count;
        }

        public Session? Last(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            return sessions.SessionList.LastOrDefault();
        }

        public bool HasPreviousSession(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            return sessions.CurrentSessionIndex > 0;
        }

        public bool HasNextSession(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            return sessions.CurrentSessionIndex < sessions.SessionList.Count - 1;
        }
    }
}
