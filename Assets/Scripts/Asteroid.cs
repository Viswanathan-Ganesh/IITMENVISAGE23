using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.collider.tag == "Player" || collision.collider.tag == "Ground") 
            {
                Destroy(gameObject);
            }
        }
    }
}
