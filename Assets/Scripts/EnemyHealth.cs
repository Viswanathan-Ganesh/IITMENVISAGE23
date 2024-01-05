using TMPro;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject healthBar;
    public float barSize;

    [SerializeField] private GameObject damagePopup;

    public Vector3 offset;

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
            ShowFloatingText(damage);
        }
    }

    void ShowFloatingText(int damage)
    {
        GameObject dmgPopup = Instantiate(damagePopup, transform.position + offset, Quaternion.identity);
        TextMeshPro txt = dmgPopup.GetComponent<TextMeshPro>();
        txt.SetText(damage.ToString());
        dmgPopup.GetComponent<DmgPopup>().textColor = txt.color;
    }
}
