using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<ScoreSystem>().coins++;
                Destroy(gameObject);
            }
        }
    }
}
