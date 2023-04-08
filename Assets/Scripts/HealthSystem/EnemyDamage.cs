using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public HealthSystem healthSystem;
    public int damage = 25;
    public int _attackcooldown = 1;
    private float _attacknext = 0.0f;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time > _attacknext )
        {
            _attacknext = Time.time + _attackcooldown;
            healthSystem.TakeDamage(damage);
        }
    }
}
