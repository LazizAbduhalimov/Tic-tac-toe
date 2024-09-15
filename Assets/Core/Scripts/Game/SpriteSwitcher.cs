using System;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    [RequireComponent(typeof(Image))]
    public class SpriteSwitcher : MonoBehaviour
    {
        public Sprite First;
        public Sprite Second;
        
        private Image Image; 

        public void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void SwitchSprite()
        {
            Image.sprite = Image.sprite == First ? Second : First;
        }
    }
}