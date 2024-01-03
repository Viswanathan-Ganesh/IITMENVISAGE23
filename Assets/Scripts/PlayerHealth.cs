using Unity.VisualScripting;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject healthBar;
    public float healthBarSize;

    void Update()
    {
        healthBar.transform.localScale = new Vector3(health * healthBarSize / maxHealth, healthBar.transform.localScale.y, 0f);
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log("Player health: " +  health);
    }
    
}
