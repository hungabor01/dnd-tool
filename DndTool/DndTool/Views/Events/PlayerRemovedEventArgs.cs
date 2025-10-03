using DndTool.Models;

namespace DndTool.Views.Events
{
    public class PlayerRemovedEventArgs
    {
        public Player RemovedPlayer { get; }

        public PlayerRemovedEventArgs(Player removedPlayer)
        {
            ArgumentNullException.ThrowIfNull(removedPlayer, nameof(removedPlayer));

            RemovedPlayer = removedPlayer;
        }
    }
}
