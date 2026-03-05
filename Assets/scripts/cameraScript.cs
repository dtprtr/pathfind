using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private float zoomMin = 5f;
    [SerializeField] private float zoomMax = 20f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomLerpSpeed = 10f;

    private AllControls controls;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbital;
    private Vector2 scrollDelta;

    private float targetZoom;
    private float currentZoom;

    private void Awake()
    {
        controls = new AllControls();
        controls.Enable();
        controls.camera.mouseZoom.performed += HandleMouseScroll;

        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponent<CinemachineCamera>();
        orbital = cam.GetComponent<CinemachineOrbitalFollow>();

        targetZoom = currentZoom = orbital.Radius;
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<Vector2>();
        Debug.Log($"Mouse Scroll: {scrollDelta}");
    }

    private void Update()
    {
        if(scrollDelta.y != 0)
        {
            if (orbital != null)
            {
                targetZoom = Mathf.Clamp(orbital.Radius - scrollDelta.y * zoomSpeed, zoomMin, zoomMax);
                scrollDelta = Vector2.zero; // Reset scroll delta after processing
            }
            
        }

        float stickDelta = controls.camera.gamepadZoom.ReadValue<float>();
        if (stickDelta != 0)
        {
            targetZoom = Mathf.Clamp(orbital.Radius - stickDelta * zoomSpeed * 16 * Time.deltaTime, zoomMin, zoomMax);
        }


        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbital.Radius = currentZoom;
    }
}


