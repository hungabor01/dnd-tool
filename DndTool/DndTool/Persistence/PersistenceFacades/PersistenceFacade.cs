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

        public async Task Execute(IUndoableCommand undoableCommand)
        {
            ArgumentNullException.ThrowIfNull(undoableCommand, nameof(undoableCommand));
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await DoInSafeMode(async () =>
            {
                _commandExecutor.Execute(undoableCommand);
                await SaveCampaign();

                CommandExecuted?.Invoke(this, EventArgs.Empty);
            });
        }

        public async Task Undo()
        {
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await DoInSafeMode(async () =>
            {
                _commandExecutor.Undo();
                await SaveCampaign();

                CommandExecuted?.Invoke(this, EventArgs.Empty);
            });
        }

        public async Task Redo()
        {
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await DoInSafeMode(async () =>
            {
                _commandExecutor.Redo();
                await SaveCampaign();

                CommandExecuted?.Invoke(this, EventArgs.Empty);
            });
        }

        public async Task CreateNewCampaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            await DoInSafeMode(async () =>
            {
                _campaign = new Campaign(name);
                await SaveCampaign();
            });
        }

        public async Task LoadCampaign(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            await DoInSafeMode(async () =>
            {
                _campaign = await _campaignFileStorage.LoadFromFile<Campaign>(name);
            });
        }

        public async Task SaveSessionHistoryFolder(string? sessionHistoryFolder)
        {
            await DoInSafeMode(async () =>
            {
                Campaign.SessionHistoryFolder = sessionHistoryFolder ?? string.Empty;
                await SaveCampaign();
            });
        }

        public async Task<Player> CreateNewPlayer(string playerName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(playerName, nameof(playerName));

            return await DoInSafeMode(async () =>
            {
                var player = new Player(playerName);
                Campaign.Players.Add(player);
                await SaveCampaign();
                return player;
            });
        }

        private async Task SaveCampaign()
        {
            ArgumentNullException.ThrowIfNull(Campaign, nameof(Campaign));

            await _campaignFileStorage.SaveToFile(Campaign.Name, Campaign);
        }

        private async Task DoInSafeMode(Func<Task> action)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private async Task<T> DoInSafeMode<T>(Func<Task<T>> action)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await action();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
