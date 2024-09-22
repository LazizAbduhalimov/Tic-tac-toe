using Leopotam.EcsLite;
using UnityEngine.UI;

namespace UI
{
    public interface IButton
    {
        public void Invoke(Button button, int entityToPack, EcsWorld world);
        public void Clicked();
    }
}