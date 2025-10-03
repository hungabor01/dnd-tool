using DndTool.Models;

namespace DndTool.Commands.CampaignCommands
{
    public class ChangeLastAdministrationDateCommand : IUndoableCommand
    {
        private readonly Campaign _campaign;
        private DateTime? _oldDateTime;

        public ChangeLastAdministrationDateCommand(Campaign campaign)
        {
            ArgumentNullException.ThrowIfNull(campaign, nameof(campaign));

            _campaign = campaign;
        }

        public void Execute()
        {
            _oldDateTime = _campaign.LastAdministrationDate;
            _campaign.LastAdministrationDate = _campaign.CurrentDate;
        }

        public void Undo()
        {
            _campaign.LastAdministrationDate = _oldDateTime!.Value;
        }
    }
}
