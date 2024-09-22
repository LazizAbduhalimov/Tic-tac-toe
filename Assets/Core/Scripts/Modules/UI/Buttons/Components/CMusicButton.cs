using Leopotam.EcsLite;
using UnityEngine.UI;

namespace UI
{
    public struct CMusicButton : IButton
    {
        public ButtonHandler Handler; 

        public void Invoke(Button button, int entity, EcsWorld world)
        {
            Handler.Invoke(button, entity, world);
        }

        public void Clicked()
        {
            Handler.HandleClick<EMusicButtonClicked>();
        }
    }
}