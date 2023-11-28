using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public float attackTime;
    public float range;

    // Update is called once per frame
    void Update()
    {
        // Timer
        attackTime -= Time.deltaTime;
        if (attackTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
