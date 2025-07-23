namespace DndTool.Persistence.FileStorages
{
    internal interface IFileStorage
    {
        public Task SaveToFile<T>(string fileName, T data);

        public Task<T?> LoadFromFile<T>(string filePath);
    }
}
