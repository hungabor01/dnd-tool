using DndTool.Commands.CampaignCommands;
using DndTool.Commands.PlayerCommands;
using DndTool.Commands.SessionCommands;
using DndTool.Common;
using DndTool.Models;
using DndTool.Persistence.CommandExecutors;
using DndTool.Persistence.FileStorages;
using DndTool.Persistence.PersistenceFacades;
using DndTool.Queries;

namespace DndTool.ViewModels
{
    internal class DmToolViewModel : IDmToolViewModel
    {
        private readonly IPersistenceFacade _persistenceFacade;

        public DmToolViewModel()
        {
            var campaignFileStorage = new FileStorage(Constants.Persistence.SaveFilesPath, Constants.Persistence.CampaignFileExtension);
            var commandExecutor = new CommandExecutor();
            _persistenceFacade = new PersistenceFacade(commandExecutor, campaignFileStorage);
            _persistenceFacade.CommandExecuted += (_, args) => CommandExecuted?.Invoke(this, args);
        }

        public Campaign Campaign => _persistenceFacade.Campaign;
        public bool IsUndoEnabled => _persistenceFacade.IsUndoEnabled;
        public bool IsRedoEnabled => _persistenceFacade.IsRedoEnabled;
        public event EventHandler? CommandExecuted;

        public async Task Undo()
        {
            await _persistenceFacade.Undo();
        }

        public async Task Redo()
        {
            await _persistenceFacade.Redo();
        }

        public async Task CreateNewCampaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            await _persistenceFacade.CreateNewCampaign(name);
        }

        public async Task LoadCampaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            await _persistenceFacade.LoadCampaign(name);
        }

        public async Task ChangeCurrentDate(DateTime newDateTime)
        {
            var changeDateCommand = new ChangeCurrentDateCommand(_persistenceFacade.Campaign, newDateTime);
            await _persistenceFacade.Execute(changeDateCommand);
        }

        public async Task ChangeLastAdministrationDate()
        {
            var changeDateCommand = new ChangeLastAdministrationDateCommand(_persistenceFacade.Campaign);
            await _persistenceFacade.Execute(changeDateCommand);
        }

        public async Task CreateNewSession(string sessionName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(sessionName, nameof(sessionName));

            var createNewSessionCommand = new CreateNewSessionCommand(_persistenceFacade.Campaign.Sessions, sessionName);
            await _persistenceFacade.Execute(createNewSessionCommand);
        }

        public async Task ChangeSessionStartDate(DateTime newDateTime)
        {
            var sessionQueries = new SessionQueries();
            var currentSession = sessionQueries.GetCurrentSession(_persistenceFacade.Campaign.Sessions)!;
            var changeDateCommand = new ChangeSessionStartDateCommand(currentSession, newDateTime);
            await _persistenceFacade.Execute(changeDateCommand);
        }

        public async Task ChangeSessionEndDate(DateTime newDateTime)
        {
            var sessionQueries = new SessionQueries();
            var currentSession = sessionQueries.GetCurrentSession(_persistenceFacade.Campaign.Sessions)!;
            var changeDateCommand = new ChangeSessionEndDateCommand(currentSession, newDateTime);
            await _persistenceFacade.Execute(changeDateCommand);
        }

        public async Task ChangeSessionName(string newSessionName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newSessionName, nameof(newSessionName));

            var sessionQueries = new SessionQueries();
            var currentSession = sessionQueries.GetCurrentSession(_persistenceFacade.Campaign.Sessions)!;
            if (currentSession.Name == newSessionName)
            {
                return;
            }

            var changeSessionNameCommand = new ChangeSessionNameCommand(currentSession, newSessionName);
            await _persistenceFacade.Execute(changeSessionNameCommand);
        }

        public async Task IncrementCurrentSession()
        {
            var incrementCurrentSessionCommand = new IncrementCurrentSessionCommand(_persistenceFacade.Campaign.Sessions);
            await _persistenceFacade.Execute(incrementCurrentSessionCommand);
        }

        public async Task DecrementCurrentSession()
        {
            var decrementCurrentSessionCommand = new DecrementCurrentSessionCommand(_persistenceFacade.Campaign.Sessions);
            await _persistenceFacade.Execute(decrementCurrentSessionCommand);
        }

        public async Task SaveSessionHistoryFolder(string? sessionHistoryFolder)
        {
            await _persistenceFacade.SaveSessionHistoryFolder(sessionHistoryFolder);
        }

        public async Task<Player> CreateNewPlayer(string playerName)
        {
            return await _persistenceFacade.CreateNewPlayer(playerName);
        }

        public async Task<bool> ChangePlayerProperty(Player player, int propertyIndex, bool isPropertyName, string? value)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            if (IsEmptyNewPropertyName(player, propertyIndex, isPropertyName, value)
                || IsValueUnchanged(player, propertyIndex, isPropertyName, value))
            {
                return false;
            }

            var changePlayerPropertyCommand = new ChangePlayerPropertyCommand(player, propertyIndex, isPropertyName, value);
            await _persistenceFacade.Execute(changePlayerPropertyCommand);
            return true;
        }

        private bool IsEmptyNewPropertyName(Player player, int propertyIndex, bool isPropertyName, string? value)
        {
            return propertyIndex >= player.Properties.Count
                   && isPropertyName
                   && string.IsNullOrWhiteSpace(value);
        }

        private bool IsValueUnchanged(Player player, int propertyIndex, bool isPropertyName, string? value)
        {
            if (propertyIndex >= player.Properties.Count)
            {
                return false;
            }

            var playerProperty = player.Properties[propertyIndex];
            return isPropertyName
                ? playerProperty.PropertyName == value
                : playerProperty.PropertyValue == value;
        }

        public async Task RemovePlayer(Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var removePlayerCommand = new RemovePlayerCommand(_persistenceFacade.Campaign, player);
            await _persistenceFacade.Execute(removePlayerCommand);
        }
    }
}
