using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTr;
    private float timer;
    public float timeDiff;
    public Vector3 offset;
    // Update is called once per frame

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            transform.position = Vector3.Lerp(transform.position, playerTr.position, Time.deltaTime) + offset;
            timer = timeDiff;
        }
    }
}
