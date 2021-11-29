using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private static CameraController instance; //singleton

    public FloatSO playerSpeedOut;
    private Vector3 smoothPosition;
    private float zComp;
    private float zCompSpeed;
    public FloatSO dampParam;
    public float dampEffectScale = 0.25f;

    private void OnEnable()
    {
        if (instance != null)
        {
            Debug.LogError("MORE INSTANCES! SINGLETON PROBLEM!");
            return;
        }
        
        instance = this;
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        smoothPosition = target.position + offset;

        zComp = smoothPosition.z;

        zComp = Mathf.SmoothDamp(zComp, zComp - (dampEffectScale * playerSpeedOut.floatValue), ref zCompSpeed, dampParam.floatValue * Time.deltaTime);

        smoothPosition.z = zComp;

        transform.position = smoothPosition;

        /*
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
        */
    }

    public static void StickCameraToPlayer(Transform transform)
    {
        Debug.Log("stick camera");
        target = transform;
    }

    public static float SmoothSpeed
    {
        get
        {
            return instance.smoothSpeed;
        }
    }

    //to do ogarniêcia póŸniej, testowo
    void CameraRotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 5), 0.025f);
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        //nie dzia³a, do wyjebania albo do zmiany

        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
