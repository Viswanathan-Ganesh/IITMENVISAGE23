using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMoves : MonoBehaviour
{
    public float range;
    public GameObject hitChecker;
    public Vector3 offset;
    public float attackTime;
    public float attackTimer;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HitCheckerSpawner();
        }


        
    }

    void HitCheckerSpawner()
    {
        Instantiate(hitChecker, transform.position + offset, Quaternion.identity);
    }
}
