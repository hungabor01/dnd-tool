using DndTool.Common;

namespace DndTool.Models
{
    public class Sessions
    {
        public List<Session> SessionList { get; set; } = new();
        public int CurrentSessionIndex { get; set; } = Constants.Sessions.DefaultSessionIndex;

        public Sessions()
        {
        }
    }
}
