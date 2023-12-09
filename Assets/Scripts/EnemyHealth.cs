using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int health;


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
    public void SetHealth(int damage) // damages
    {
        if (transform.GetComponent<EnemyAI>().isDodging == false)
        {
            health -= damage;
            Debug.Log(health);
        }
    }
}
