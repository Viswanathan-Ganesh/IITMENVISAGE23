using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public float health;


    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject); // Enemy destroying condition
        }
    }

    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float damage) // damages
    {
        health -= damage;
        Debug.Log(health);
    }
}
