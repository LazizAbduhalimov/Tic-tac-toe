using Leopotam.EcsLite;
using UnityEngine.UI;

namespace UI
{
    public struct ButtonHandler
    {
        public Button Button;
        public EcsPackedEntity Entity;
        public EcsWorld EntityWorld;

        public void Invoke(Button button, int entity, EcsWorld world)
        {
            var packedEntity = world.PackEntity(entity);
            Button = button;
            Entity = packedEntity;
            EntityWorld = world;
        }

        public void HandleClick<T>() where T : struct
        {
            Entity.Unpack(EntityWorld, out var entity);
            EntityWorld.GetPool<T>().Add(entity);
        }
    }
}