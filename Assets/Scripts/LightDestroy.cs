using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestroy : MonoBehaviour
{
    public Gradient lightColor;

    private void Awake()
    {
        gameObject.GetComponent<Light>().color = lightColor.Evaluate(Random.Range(0f, 1f));
    }

    private void Update()
    {
        if (gameObject.transform.position.z <= -70) Destroy(gameObject);
    }
}
