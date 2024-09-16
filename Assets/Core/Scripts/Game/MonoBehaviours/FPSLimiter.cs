using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField] private int _fpsLimit = 60;

    public void Initialize()
    {
        Application.targetFrameRate = _fpsLimit;
    }

    public void Start()
    {
        Initialize();
    }
}
