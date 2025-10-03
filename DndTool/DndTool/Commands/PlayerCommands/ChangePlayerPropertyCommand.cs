using DndTool.Models;

namespace DndTool.Commands.PlayerCommands
{
    internal class ChangePlayerPropertyCommand : IUndoableCommand
    {
        private readonly Player _player;
        private readonly int _propertyIndex;
        private readonly bool _isPropertyName;
        private readonly string? _value;
        private PlayerProperty? _oldProperty;

        public ChangePlayerPropertyCommand(Player player, int propertyIndex, bool isPropertyName, string? value)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            _player = player;
            _propertyIndex = propertyIndex;
            _isPropertyName = isPropertyName;
            _value = value;
        }

        public void Execute()
        {
            if (IsNewProperty())
            {
                AddProperty();
                return;
            }

            var playerProperty = GetPlayerProperty();
            _oldProperty = new PlayerProperty(playerProperty.PropertyName, playerProperty.PropertyValue);

            if (IsRemoveProperty())
            {
                RemoveProperty(playerProperty);
                return;
            }

            ModifyProperty(playerProperty);
        }

        private bool IsNewProperty()
        {
            return _propertyIndex >= _player.Properties.Count;
        }

        private void AddProperty()
        {
            if (!_isPropertyName || string.IsNullOrWhiteSpace(_value))
            {
                throw new InvalidOperationException("Invalid add property data.");
            }

            _player.Properties.Add(new PlayerProperty(_value!, string.Empty));
        }

        private bool IsRemoveProperty()
        {
            return _isPropertyName && string.IsNullOrWhiteSpace(_value);
        }

        private void RemoveProperty(PlayerProperty playerProperty)
        {
            _player.Properties.Remove(playerProperty);
        }

        private void ModifyProperty(PlayerProperty playerProperty)
        {
            if (_isPropertyName)
            {
                if (string.IsNullOrWhiteSpace(_value))
                {
                    throw new InvalidOperationException("Invalid modify property data.");
                }

                playerProperty.PropertyName = _value;
            }
            else
            {
                playerProperty.PropertyValue = _value ?? string.Empty;
            }
        }

        public void Undo()
        {
            if (_oldProperty == null)
            {
                _player.Properties.RemoveAt(_player.Properties.Count - 1);
                return;
            }

            if (IsRemoveProperty())
            {
                _player.Properties.Insert(_propertyIndex, _oldProperty);
                return;
            }

            var playerProperty = GetPlayerProperty();
            playerProperty.PropertyName = _oldProperty.PropertyName;
            playerProperty.PropertyValue = _oldProperty.PropertyValue;
        }

        private PlayerProperty GetPlayerProperty()
        {
            return _player.Properties[_propertyIndex];
        }
    }
}
