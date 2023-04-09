using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    protected bool dead;
    protected float health;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public float GetHealth()
    {
        return health;
    }
    
    public float GetStartingHealth()
    {
        return startingHealth;
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
