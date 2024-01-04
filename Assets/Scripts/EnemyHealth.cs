using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject healthBar;
    public float barSize;


    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject); // Enemy destroying condition
        }
        
        healthBar.transform.localScale = new Vector3(health * barSize / maxHealth, healthBar.transform.localScale.y, 0f);
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
