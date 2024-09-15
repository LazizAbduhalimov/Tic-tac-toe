using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [Serializable]
    public struct CGhost
    {
        public GameObject OPrefab;
        public GameObject XPrefab;

        public void Invoke (GameObject oPrefab, GameObject xPrefab)
        {
            OPrefab = oPrefab;
            XPrefab = xPrefab;
        }
    }
}