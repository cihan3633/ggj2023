using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent _agent;
    Transform _player;
    public LayerMask ground, player;

    //Destination
    public Vector3 destinationPoint;
    public bool destinationPointSet;


    //public float timeBetweenAttacks;
    //private bool alreadyAttacked;
    //public GameObject sphere;

    //Ranges
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float patrolRange;
    public float patrolingSpeed = 1;
    public float chaseSpeed = 3.5f;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        //Returns true if there are any colliders overlapping the sphere defined by Sight Range and Attack Range 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        //Patrol--Chase--Attack

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        // if (playerInSightRange && playerInAttackRange) Attack();
        EnemyAnim();
    }

    //Methods
    void Patroling()
    {
        if (!destinationPointSet)
        {
            SearchNewPosition();
        }

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        //İf distance between npc and destination point is less than 1.0f, It is set false.As a result npc back to patroling.
        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if (distanceToDestinationPoint.sqrMagnitude < 1.0f)
        {
            destinationPointSet = false;

        }
    }

    void SearchNewPosition()
    {
        if (_agent.speed > patrolingSpeed)
        {
            _agent.speed = patrolingSpeed;
        }
        float randomPositionX = UnityEngine.Random.Range(-patrolRange, patrolRange);
        float randomPositionZ = UnityEngine.Random.Range(-patrolRange, patrolRange);

        destinationPoint = new Vector3(transform.position.x + randomPositionX, transform.position.y, transform.position.z + randomPositionZ);


        //Returns true if the ray intersects with a Collider, otherwise false.
        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            destinationPointSet = true;
        }

    }

    void EnemyAnim()
    {
        if (playerInSightRange == false)
        {
            anim.SetBool("runingEnemy", false);
        }
        if (playerInSightRange == true)
        {
            anim.SetBool("runingEnemy", true);
        }
    }

    void ChasePlayer()
    {
        _agent.speed = chaseSpeed;
        _agent.SetDestination(_player.position);
        destinationPointSet = false;
        Vector3 distance = transform.position - _player.position;

        if (distance.sqrMagnitude < 25)
        {
            anim.SetBool("attackingEnemy", true);
        }
        else
        {
            anim.SetBool("attackingEnemy", false);
        }
    }

    //void Attack()
    //{

    //}



}
