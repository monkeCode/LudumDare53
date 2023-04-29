
namespace Interfaces
{
    public interface ISingleton<T> where T : ISingleton<T>
    {
        public T Instance { get; }
    }
}
