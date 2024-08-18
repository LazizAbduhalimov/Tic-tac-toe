using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MarkTurnUIShower : MonoBehaviour
    {
        public Image xImage;
        public Image oImage;

        public void SwitchMark(Marks type)
        {
            switch (type)
            {
                case Marks.X:
                    xImage.gameObject.SetActive(true);
                    oImage.gameObject.SetActive(false);
                    break;
                case Marks.O:
                    xImage.gameObject.SetActive(false);
                    oImage.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}