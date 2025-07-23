using System.Text.Json;

namespace DndTool.Persistence.FileStorages
{
    internal class FileStorage : IFileStorage
    {
        private readonly string _saveFilesPath;
        private readonly string _fileExtension;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = true
        };

        public FileStorage(string saveFilesPath, string fileExtension)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(saveFilesPath, nameof(saveFilesPath));
            ArgumentException.ThrowIfNullOrWhiteSpace(fileExtension, nameof(fileExtension));

            _saveFilesPath = saveFilesPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            _fileExtension = fileExtension.StartsWith('.')
                ? fileExtension :
                "." + fileExtension;
        }

        public async Task SaveToFile<T>(string fileName, T data)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

            var filePath = GetFilePath(fileName);
            await using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, data, _jsonSerializerOptions);
        }

        public async Task<T?> LoadFromFile<T>(string fileName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

            var filePath = GetFilePath(fileName);
            if (!File.Exists(filePath))
            {
                return default;
            }

            await using var stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        private string GetFilePath(string fileName)
        {
            if (!Directory.Exists(_saveFilesPath))
            {
                Directory.CreateDirectory(_saveFilesPath);
            }

            return Path.Combine(_saveFilesPath, fileName + _fileExtension);
        }
    }
}
