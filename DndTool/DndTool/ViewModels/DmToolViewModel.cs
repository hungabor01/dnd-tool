using DndTool.Commands.CampaignCommands;
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

        public async Task CreateNewSession(string sessionName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(sessionName, nameof(sessionName));

            var createNewSessionCommand = new CreateNewSessionCommand(_persistenceFacade.Campaign.Sessions, sessionName);
            await _persistenceFacade.Execute(createNewSessionCommand);
        }

        public async Task ChangeSessionStartDate(DateTime newDateTime)
        {
            var sessionQueries = new SessionQueries();
            var currentSession = sessionQueries.CurrentSession(_persistenceFacade.Campaign.Sessions);
            var changeDateCommand = new ChangeSessionStartDateCommand(currentSession, newDateTime);
            await _persistenceFacade.Execute(changeDateCommand);
        }

        public async Task ChangeSessionEndDate(DateTime newDateTime)
        {
            var sessionQueries = new SessionQueries();
            var currentSession = sessionQueries.CurrentSession(_persistenceFacade.Campaign.Sessions);
            var changeDateCommand = new ChangeSessionEndDateCommand(currentSession, newDateTime);
            await _persistenceFacade.Execute(changeDateCommand);
        }

        public async Task ChangeSessionName(string newSessionName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newSessionName, nameof(newSessionName));

            var sessionQueries = new SessionQueries();
            var currentSession = sessionQueries.CurrentSession(_persistenceFacade.Campaign.Sessions);
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
    }
}
