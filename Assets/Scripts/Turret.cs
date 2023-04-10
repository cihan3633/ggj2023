using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] Transform _player;
    [SerializeField] Transform muzzle;
    private bool alreadyAttacked;
    public GameObject projectile;
    public float timeBetweenAttacks = 2;
    private float health = 48;


    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        float sqrDistance = (_player.position - transform.position).sqrMagnitude;
        if (sqrDistance < 400)
        {
            transform.LookAt(_player);
            if (!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(projectile, muzzle.position, muzzle.rotation).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
                rb.AddForce(transform.up * 7f, ForceMode.Impulse);

                Destroy(rb.gameObject, 2);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
