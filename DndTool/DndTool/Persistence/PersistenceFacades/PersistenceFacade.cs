using DndTool.Commands;
using DndTool.Models;
using DndTool.Persistence.FileStorages;
using ICommandExecutor = DndTool.Persistence.CommandExecutors.ICommandExecutor;

namespace DndTool.Persistence.PersistenceFacades
{
    internal class PersistenceFacade : IPersistenceFacade
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
        private readonly ICommandExecutor _commandExecutor;
        private readonly IFileStorage _campaignFileStorage;

        private Campaign? _campaign;

        public Campaign Campaign => _campaign ?? throw new InvalidOperationException("Campaign has not been loaded yet. Call LoadCampaign first.");
        public bool IsUndoEnabled => _commandExecutor.IsUndoEnabled;
        public bool IsRedoEnabled => _commandExecutor.IsRedoEnabled;
        public event EventHandler? CommandExecuted;

        public PersistenceFacade(ICommandExecutor commandExecutor, IFileStorage campaignFileStorage)
        {
            ArgumentNullException.ThrowIfNull(commandExecutor, nameof(commandExecutor));
            ArgumentNullException.ThrowIfNull(campaignFileStorage, nameof(campaignFileStorage));

            _commandExecutor = commandExecutor;
            _campaignFileStorage = campaignFileStorage;
        }

        public async Task CreateNewCampaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            await _semaphoreSlim.WaitAsync();
            try
            {
                _campaign = new Campaign(name);
                await SaveCampaign();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task LoadCampaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            await _semaphoreSlim.WaitAsync();
            try
            {
                _campaign = await _campaignFileStorage.LoadFromFile<Campaign>(name);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task Execute(IUndoableCommand undoableCommand)
        {
            ArgumentNullException.ThrowIfNull(undoableCommand, nameof(undoableCommand));
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await _semaphoreSlim.WaitAsync();
            try
            {
                _commandExecutor.Execute(undoableCommand);
                await SaveCampaign();

                CommandExecuted?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task Undo()
        {
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await _semaphoreSlim.WaitAsync();
            try
            {
                _commandExecutor.Undo();
                await SaveCampaign();

                CommandExecuted?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task Redo()
        {
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await _semaphoreSlim.WaitAsync();
            try
            {
                _commandExecutor.Redo();
                await SaveCampaign();

                CommandExecuted?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private async Task SaveCampaign()
        {
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await _campaignFileStorage.SaveToFile(Campaign.Name, Campaign);
        }
    }
}
