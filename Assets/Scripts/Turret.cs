using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
   [SerializeField] Transform _player;
   private bool alreadyAttacked;
   public GameObject projectile;
   public float timeBetweenAttacks = 2;
    
   void Update()
   {
      transform.LookAt(_player);
      if (!alreadyAttacked)
      {
         Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
         rb.AddForce(transform.forward*25f,ForceMode.Impulse);
         rb.AddForce(transform.up*7f,ForceMode.Impulse);

         alreadyAttacked = true;
         Invoke(nameof(ResetAttack), timeBetweenAttacks);
         
         
      }
   } void ResetAttack()
   {
      alreadyAttacked = false;

   }
}
