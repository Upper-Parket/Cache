namespace Interview.Contracts;

public interface ICacheBalancer
{
    void AddShard(ICache cache);
    void RemoveShard(ICache cache);
}