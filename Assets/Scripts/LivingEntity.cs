using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    protected bool dead;
    protected float health;

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (!dead && health <= 0)
        {
            health = 0;
            Die();
        }
    }

    protected void Die()
    {
        // death effect...
        dead = true;
        Destroy(gameObject);
    }
}
