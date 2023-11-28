using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMoves : MonoBehaviour
{
    public GameObject hitChecker;
    public Vector3 offset;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HitCheckerSpawner();
        }
        
    }

    void HitCheckerSpawner()
    {
        Instantiate(hitChecker, transform.position + offset, Quaternion.identity);
    }
}
