using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDamage : MonoBehaviour
{
    Player player;
    private EnemyController enemyController;
    [SerializeField] private AudioClip zombieClipAttack;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        player = FindObjectOfType<Player>();
    }

    public void Damage(int amount)
    {
        player.TakeDamage(amount);
        enemyController.anim.SetBool("attackingEnemy", false);
        SoundManager.Instance.playAudio(zombieClipAttack, transform.position);
    }
}