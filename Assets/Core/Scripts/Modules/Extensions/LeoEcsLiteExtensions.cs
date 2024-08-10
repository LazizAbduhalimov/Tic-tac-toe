using Leopotam.EcsLite.Di;

public static class LeoEcsLiteExtensions
{
    public static bool TryGetFirst<T>(this EcsFilterInject<Inc<T>> filter, out T component) where T : struct
    {
        foreach (var entity in filter.Value)
        {
            component = filter.Pools.Inc1.Get(entity);
            return true;
        }
        component = default;
        return false;
    }
}