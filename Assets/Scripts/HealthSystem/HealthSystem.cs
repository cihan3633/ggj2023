using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   public int health;
   public int maxHealth = 100;

   private void Start()
   {
      health = maxHealth;

   }
   // how much damage the enemy or player takes
   public void TakeDamage(int amount)
   {
      health -= amount;
      if (health <= 0)
      {
         Destroy(gameObject);
      }
      
   }
   
   
}
