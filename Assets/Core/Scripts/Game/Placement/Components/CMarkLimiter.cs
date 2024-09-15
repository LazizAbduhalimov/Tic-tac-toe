using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public struct CMarkLimiter
    {
        public Queue<GameObject> XMarks;
        public Queue<GameObject> OMarks;

        public void Invoke()
        {
            XMarks = new Queue<GameObject>();
            OMarks = new Queue<GameObject>();
        }
    }
}