using DndTool.Models;

namespace DndTool.Views.Controls
{
    internal class PlayerPropertyTextBox : TextBox
    {
        public Player Player { get; }
        public int PropertyIndex { get; }
        public bool IsPropertyName { get; }

        public PlayerPropertyTextBox(Player player, int propertyIndex, bool isPropertyName)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            Player = player;
            PropertyIndex = propertyIndex;
            IsPropertyName = isPropertyName;
        }
    }
}
