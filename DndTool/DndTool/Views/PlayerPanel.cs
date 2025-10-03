using DndTool.Models;
using DndTool.Views.Events;

namespace DndTool.Views
{
    public partial class PlayerPanel : UserControl
    {
        private readonly Player _player;

        public EventHandler<PlayerRemovedEventArgs>? OnPlayerRemoved;

        public PlayerPanel(Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            InitializeComponent();

            _player = player;

            PlayerNameLabel.Text = player.Name;
        }

        private void RemovePlayerButton_Click(object sender, EventArgs e)
        {
            OnPlayerRemoved?.Invoke(this, new PlayerRemovedEventArgs(_player));
        }
    }
}
