using System;
using UnityEngine;

public class CameraFieldViewChanger : MonoBehaviour
{
    [SerializeField] private float _landscapeFieldOfView = 30f;
    [SerializeField] private float _portraitFieldOfView = 60f;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        SetFieldOfViewMultiplierByAspectRatio();
    }

    private void Update()
    {
        SetRotationView();
    }

    private void SetFieldOfViewMultiplierByAspectRatio()
    {
        var defaultView = 16f / 9f;
        var aspectRatio = Screen.width > Screen.height ? (float)Screen.width / Screen.height : (float)Screen.height / Screen.width;

        var multiplier = Mathf.Clamp(aspectRatio / defaultView, 0.8f, 1.2f);
        
        _portraitFieldOfView *= multiplier;
        _landscapeFieldOfView *= multiplier;
    }

    private void SetRotationView()
    {
        if (Screen.width > Screen.height)
            _camera.fieldOfView = _landscapeFieldOfView;
        else
            _camera.fieldOfView = _portraitFieldOfView;
    }
}
