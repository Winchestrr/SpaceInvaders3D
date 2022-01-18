using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestroy : MonoBehaviour
{
    public Gradient lightColor;
    public bool isAlreadyGroovin = false;

    private void Awake()
    {
        gameObject.GetComponent<Light>().color = lightColor.Evaluate(Random.Range(0f, 1f));
    }

    private void Update()
    {
        if (gameObject.transform.position.z <= -70) Destroy(gameObject);

        if (SaveData.isBoogie && !isAlreadyGroovin) BoogieModeON();
    }

    private void BoogieModeON()
    {
        isAlreadyGroovin = true;
        InvokeRepeating("RandomLight", 0, (60f / 132f));
    }

    private void RandomLight()
    {
        gameObject.GetComponent<Light>().color = Random.ColorHSV(0, 1, 1, 1, 0, 1);
    }
}
