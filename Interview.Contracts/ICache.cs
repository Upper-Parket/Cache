namespace Interview.Contracts;

public interface ICache
{
    string? GetPage(string key);
    void SetPage(string key, string value);
}