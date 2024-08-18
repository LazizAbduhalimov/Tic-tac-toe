using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Returns first level children of game object
    /// </summary>
    /// <returns></returns>
    public static GameObject[] GetAllChildren(this GameObject gameObject)
    {
        return Enumerable.Range(0, gameObject.transform.childCount) 
            .Select(i => gameObject.transform.GetChild(i).gameObject)
            .ToArray();
    }

    public static GameObject[] GetInactiveChildren(this GameObject gameObject)
    {
        return gameObject.GetAllChildren()
            .Where(go => !go.activeInHierarchy)
            .ToArray();
    }
    
    public static GameObject[] GetActiveChildren(this GameObject gameObject)
    {
        return gameObject.GetAllChildren()
            .Where(go => go.activeInHierarchy)
            .ToArray();
    }
}