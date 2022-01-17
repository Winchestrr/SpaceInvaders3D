using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestroy : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.transform.position.z <= -70) Destroy(gameObject);
    }
}
