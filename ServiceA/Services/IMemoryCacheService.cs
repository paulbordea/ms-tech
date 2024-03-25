namespace ServiceA.Services
{
    public interface IMemoryCacheService<T>
    {
        public void Set(string key, T value);
        public T Get(string key);
    }
}
