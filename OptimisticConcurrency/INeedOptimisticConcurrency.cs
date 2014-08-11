namespace Ghidello.MongoDB.OptimisticConcurrency
{
    public interface INeedOptimisticConcurrency
    {
        int Version { get; set; }
    }
}