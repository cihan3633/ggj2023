using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDamage : MonoBehaviour
{
    public Health _health;
    public GameObject _player; 
    private EnemyController _enemyController;
    [SerializeField] private AudioClip zombieClipAttack;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        
    }


    public void Damage(int amount)
    {
        
        _health.health -= amount;
        if (_health.health <= 0)
        {
            Destroy(_player); 
            _enemyController.anim.SetBool("attackingEnemy",false);
            SoundManager.Instance.playAudio(zombieClipAttack,transform.position);
        }
      
    }

    
}















//public int damage = 25;
   // public int _attackcooldown = 1;
    //private float _attacknext = 0.0f;

    //private Animator anim;

  //  private void Start()
  //  {
       // anim = GetComponent<Animator>();
    //}

   // public void OnCollisionStay(Collision collision)
   // {
        //&& Time.time > _attacknext
        //if (collision.gameObject.tag == "Player" && Time.time > _attacknext  )
       // {
            //_attacknext = Time.time + _attackcooldown;
            //health.TakeDamage(25);
            
       // }
        // else
        // {
        //     anim.SetBool("attackingEnemy",false);
        // }
        // anim.SetBool("attackingEnemy",true);
   // }

    

