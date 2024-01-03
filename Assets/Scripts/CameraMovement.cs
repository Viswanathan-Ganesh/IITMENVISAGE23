using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTr;
    private float timer;
    public float timeDiff;
    public Vector3 offset;
    // Update is called once per frame
    private void Start()
    {
        transform.position = transform.position + offset;

    }
    void Update()
    {   
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            transform.position = Vector3.Lerp(transform.position, playerTr.position + offset, Time.deltaTime);
            timer = timeDiff;
            if(playerTr.GetComponent<PlayerHealth>().health <= 0f) 
            {
                Destroy(gameObject);
            }
        }

    }
}
