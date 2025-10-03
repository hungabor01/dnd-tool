using DndTool.Common;

namespace DndTool.Models
{
    public class Session
    {
        public int IndexNumber { get; set; } = Constants.Sessions.DefaultSessionIndex;
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public Session()
        {
        }

        public Session(int indexNumber, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            IndexNumber = indexNumber;
            Name = name;
        }
    }
}
