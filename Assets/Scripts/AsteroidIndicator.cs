using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidIndicator : MonoBehaviour
{
    public float delay;

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            Destroy(gameObject);
        }
    }
}
