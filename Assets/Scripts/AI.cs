using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    private Animator animator;
    private string currentState;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, WhatIsPlayer;

    public float health;
    public GameObject Weapon;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float cooldownattack;
    bool alreadyattacked;
    public int damage;

    //states
    public float sightRange, AttackRange;
    public bool PlayerInSightRange, playerInAttackRange;

    //Animation Enemy States
    const string ENEMY_WALK = "Enemy_Walk";
    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!PlayerInSightRange && !playerInAttackRange)
        {
            Patroling();
            ChangeAnimationState(ENEMY_WALK);
        }
        if (PlayerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            ChangeAnimationState(ENEMY_WALK);
        }
        if (PlayerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            ChangeAnimationState(ENEMY_WALK);
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkpoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkpoint()
    {
        float RandomZ = Random.Range(-walkPointRange, walkPointRange);
        float RandomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyattacked)
        {
            alreadyattacked = true;
            Invoke(nameof(ResetAttack), cooldownattack);

            IEnumerator AttackingPlayer()
            {
                RaycastHit hit;
                if (Physics.Raycast(Weapon.transform.position, Weapon.transform.forward, out hit, sightRange))
                {
                    Debug.Log(hit.transform);

                    PlayerMovement playerscript = hit.transform.GetComponent<PlayerMovement>();
                    if (playerscript != null)
                    {
                        playerscript.TakeDamage(damage);
                    }
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyattacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
