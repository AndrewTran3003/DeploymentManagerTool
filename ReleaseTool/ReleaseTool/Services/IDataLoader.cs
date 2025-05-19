namespace ReleaseTool.Services;

public interface IDataLoader
{
    public Task<IEnumerable<T>> LoadCollectionAsync<T>(string filePath) where T : class, new();
}