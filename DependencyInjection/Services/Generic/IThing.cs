namespace DependencyInjection.Services.Generic
{
    public interface IThing<T>
    {
        string GetName { get; }
    }
}