using DndTool.Models;

namespace DndTool.Commands.CampaignCommands
{
    public class ChangeCurrentDateCommand : IUndoableCommand
    {
        private readonly Campaign _campaign;
        private readonly DateTime _newDateTime;
        private DateTime? _oldDateTime;

        public ChangeCurrentDateCommand(Campaign campaign, DateTime newDateTime)
        {
            ArgumentNullException.ThrowIfNull(campaign, nameof(campaign));

            _campaign = campaign;
            _newDateTime = newDateTime;
        }

        public void Execute()
        {
            _oldDateTime = _campaign.CurrentDate;
            _campaign.CurrentDate = _newDateTime;
        }

        public void Undo()
        {
            if (_oldDateTime.HasValue)
            {
                _campaign.CurrentDate = _oldDateTime.Value;
            }
        }
    }
}
