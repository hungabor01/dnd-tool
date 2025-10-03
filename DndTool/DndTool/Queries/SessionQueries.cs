using DndTool.Models;

namespace DndTool.Queries
{
    internal class SessionQueries
    {
        public Session? GetCurrentSession(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            var currentSessionIndex = sessions.CurrentSessionIndex;
            return sessions.SessionList.SingleOrDefault(session => session.IndexNumber == currentSessionIndex);
        }

        public Session? GetLastSession(Sessions sessions)
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
