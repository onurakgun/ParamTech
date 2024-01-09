namespace ParamTech.Core.Persistence.CrosCuttingConcerns.Caching;
public interface ICacheManager
{
    T Get<T>(string key);

    object Get(string key);

    void Add(string key, object data, int duration);

    bool IsAdd(string key);

    void Remove(string key);

    void RemoveByPattern(string pattern);

    void AllCacheRemove();

    void Set(string key, object data, int duration);

    void PatternRemove(string pattern);
}