namespace DndTool.Models
{
    public class PlayerProperty
    {
        private string _propertyName = string.Empty;

        public string PropertyName
        {
            get => _propertyName;
            set => _propertyName = value ?? throw new InvalidOperationException("Player property name cannot be null.");
        }

        public string? PropertyValue { get; set; }

        public PlayerProperty()
        {
        }

        public PlayerProperty(string propertyName, string? propertyValue)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName, nameof(propertyName));

            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}
