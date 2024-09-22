using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public static class UIUtils
    {
        public static ref T InitButton<T>(Button button, EcsPool<T> pool, int? entity = null) where T : struct, IButton
        {
            var world = pool.GetWorld();
            entity ??= world.NewEntity();
            ref var buttonComponent = ref pool.Add(entity.Value);
            buttonComponent.Invoke(button, entity.Value, world);
            button.onClick.AddListener(buttonComponent.Clicked);
            return ref buttonComponent;
        }
        
        public static void ChangeButtonColor(Button button, Color color)
        {
            var colors = button.colors;
            colors.normalColor = color;
            button.colors = colors;
        }
    }
}