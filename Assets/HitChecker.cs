using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public float attackTime;
    public float range;
    public Vector3 sizeInc;


    // Update is called once per frame
    void Update()
    {
        // Timer
        attackTime -= Time.deltaTime;
        if (attackTime <= 0)
        {
            Destroy(gameObject);
        }
        if (gameObject.transform.localScale.x <= range)
        {
            gameObject.transform.localScale += sizeInc;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("I hit");
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
