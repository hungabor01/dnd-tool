using DndTool.Models;

namespace DndTool.Commands.PlayerCommands
{
    internal class RemovePlayerCommand : IUndoableCommand
    {
        private readonly Campaign _campaign;
        private readonly Player _player;
        private int _playerIndex;

        public RemovePlayerCommand(Campaign campaign, Player player)
        {
            ArgumentNullException.ThrowIfNull(campaign, nameof(campaign));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            _campaign = campaign;
            _player = player;
        }

        public void Execute()
        {
            _playerIndex = _campaign.Players.IndexOf(_player);
            _campaign.Players.Remove(_player);
        }

        public void Undo()
        {
            _campaign.Players.Insert(_playerIndex, _player);
        }
    }
}
