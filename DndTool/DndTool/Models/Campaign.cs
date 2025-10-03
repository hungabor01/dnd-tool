namespace DndTool.Models
{
    public class Campaign
    {
        public string Name { get; set; } = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public DateTime LastAdministrationDate { get; set; } = DateTime.Now;
        public string SessionHistoryFolder { get; set; } = string.Empty;
        public Sessions Sessions { get; set; } = new();
        public IList<Player> Players { get; set; } = new List<Player>();

        public Campaign()
        {
        }

        public Campaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Name = name;
        }
    }
}
