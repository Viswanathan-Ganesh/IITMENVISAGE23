using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DmgPopup : MonoBehaviour
{
    [SerializeField] private float speed;

    private float disappearTimer;
    public float disappearTime;
    [SerializeField] private float disappearSpeed;

    public Color textColor;

    void Start()
    {
        disappearTimer = disappearTime;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, speed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            gameObject.GetComponent<TextMeshPro>().color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
