namespace DndTool.Models
{
    public class Player
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public IList<PlayerProperty> Properties { get; set; } = new List<PlayerProperty>();

        public Player()
        {
        }

        public Player(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Name = name;

            InitializeStandardProperties();
        }

        private void InitializeStandardProperties()
        {
            Properties.Add(new PlayerProperty("Character name", string.Empty));
            Properties.Add(new PlayerProperty("Race", string.Empty));
            Properties.Add(new PlayerProperty("Class", string.Empty));
            Properties.Add(new PlayerProperty("Passive PER", string.Empty));
        }
    }
}
