using DndTool.Models;
using DndTool.Queries;

namespace DndTool.Commands.SessionCommands
{
    public class DecrementCurrentSessionCommand : IUndoableCommand
    {
        private readonly Sessions _sessions;

        public DecrementCurrentSessionCommand(Sessions sessions)
        {
            ArgumentNullException.ThrowIfNull(sessions, nameof(sessions));

            _sessions = sessions;
        }

        public void Execute()
        {
            var sessionQueries = new SessionQueries();
            if (!sessionQueries.HasPreviousSession(_sessions))
            {
                throw new InvalidOperationException("There is no previous session to decrement to.");
            }

            _sessions.CurrentSessionIndex--;
        }

        public void Undo()
        {
            var sessionQueries = new SessionQueries();
            if (!sessionQueries.HasNextSession(_sessions))
            {
                throw new InvalidOperationException("There is no next session to increment to.");
            }

            _sessions.CurrentSessionIndex++;
        }
    }
}
