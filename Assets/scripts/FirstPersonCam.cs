using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] private float SensitivityX = 100f;
    [SerializeField] private float SensitivityY = 100f;
    [SerializeField] private character_movement playerMovement;
   
    private AllControls controls;
    private CinemachineCamera cam;
    private CinemachineRotateWithFollowTarget rotateWithFollowTarget;







    private void Awake()
    {
        controls = new AllControls();
        controls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    private void Update()
    {
    
    
    }
}


