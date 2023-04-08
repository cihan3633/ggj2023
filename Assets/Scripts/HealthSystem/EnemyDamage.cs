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

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay(Collision collision)
    {
        //&& Time.time > _attacknext
        if (collision.gameObject.tag == "Player"  )
        {
            anim.SetBool("attackingEnemy",true);
            //_attacknext = Time.time + _attackcooldown;
            
        }
        else
        {
            anim.SetBool("attackingEnemy",false);
        }
    }

    
}
