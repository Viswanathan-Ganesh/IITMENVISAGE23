using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log("Player health: " +  health);
    }
}
