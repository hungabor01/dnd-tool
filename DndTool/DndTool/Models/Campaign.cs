namespace DndTool.Models
{
    public class Campaign
    {
        public string Name { get; set; } = "Campaign";
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public Sessions Sessions { get; set; } = new Sessions();
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
