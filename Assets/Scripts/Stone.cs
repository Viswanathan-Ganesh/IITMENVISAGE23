using UnityEngine;

public class Stone : MonoBehaviour
{
    public int stoneDamage;
    public float destroyDelay;
    private float timer;
    private void Update()
    {
        if (timer > 0) 
        {
            timer -= Time.deltaTime;
        }
        
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            timer = destroyDelay;
            if (collision.tag == "Player")
            {
                collision.GetComponent<PlayerHealth>().Damage(stoneDamage);

            }

        }
    }
}
