using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
   private Animator anim;
   public NavMeshAgent _agent;
   [SerializeField] Transform _player;
   public LayerMask ground, player;

   //Destination
   public Vector3 destinationPoint;
   private bool destinationPointSet;


   //public float timeBetweenAttacks;
   //private bool alreadyAttacked;
   //public GameObject sphere;

   //Ranges
   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;
   public float patrolRange;
   //public float patrolingSpeed;

   private void Awake()
   {
      _agent = GetComponent<NavMeshAgent>();
      anim = GetComponent<Animator>();
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
     enemyAnim();
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

      //İf distance between npc and player is less than 1.0f, It is set false.As a result npc back to patroling.
      Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
      if (distanceToDestinationPoint.magnitude < 1.0f)
      {
         destinationPointSet = false;
         
      }
   }

   void SearchNewPosition()
   {
      float randomPositionX = UnityEngine.Random.Range(-patrolRange, patrolRange);
      float randomPositionZ = UnityEngine.Random.Range(-patrolRange, patrolRange);

      destinationPoint = new Vector3(transform.position.x + randomPositionX, transform.position.y,transform.position.z + randomPositionZ);
      //_agent.SetDestination(destinationPoint);

      //Returns true if the ray intersects with a Collider, otherwise false.
      if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
      {
         destinationPointSet = true;
      }

   }

   void enemyAnim()
   {
      anim.SetBool("runingEnemy",destinationPointSet);
   }

   void ChasePlayer()
   {
      _agent.SetDestination(_player.position);
   }

   //void Attack()
   //{

   //}



} 
