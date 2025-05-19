using System.Text.Json;

namespace ReleaseTool.Services;

public class DefaultDataLoader : IDataLoader
{
    public async Task<IEnumerable<T>> LoadCollectionAsync<T>(string filePath) where T : class, new()
    {
        try
        {
            var path = Path.Combine(Environment.CurrentDirectory, filePath);
            using var streamReader = new StreamReader(path);
            
            var stringData = await streamReader.ReadToEndAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<T>>(stringData);
            if (result != null)
                return result;
        }
        catch (Exception _)
        {
            return [];
        }

        return [];
    }
}