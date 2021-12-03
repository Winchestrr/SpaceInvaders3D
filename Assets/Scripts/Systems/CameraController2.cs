using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController2 : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public AnimationCurve fovCurve;

    public float baseFOV = 60;
    public float fastFOV = 68;
    public float slowFOV = 52;
    public float fovSpeed = 10;

    private void Start()
    {
        baseFOV = mainCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        ChangeCameraFOV();
        //ChangeCameraFOVcurve();
    }

    void ChangeCameraFOV()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.m_Lens.FieldOfView = Mathf.Lerp(mainCamera.m_Lens.FieldOfView, fastFOV, fovSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            mainCamera.m_Lens.FieldOfView = Mathf.Lerp(mainCamera.m_Lens.FieldOfView, slowFOV, fovSpeed * Time.deltaTime);
        }
        else
        {
            mainCamera.m_Lens.FieldOfView = Mathf.Lerp(mainCamera.m_Lens.FieldOfView, baseFOV, fovSpeed * Time.deltaTime);
        }
    }

    void ChangeCameraFOVcurve()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.m_Lens.FieldOfView = fovCurve.Evaluate(fovSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //mainCamera.m_Lens.FieldOfView = 
        }
        else
        {
            //mainCamera.m_Lens.FieldOfView = 
        }
    }
}
