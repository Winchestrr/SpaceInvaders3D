using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController instance;
    static Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("Instance problem");
    }
    private void LateUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }

    public static void StickCameraToPlayer(Transform transform)
    {
        target = transform;
    }

    //to do ogarniêcia póŸniej, testowo
    void CameraRotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 5), 0.025f);
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
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
