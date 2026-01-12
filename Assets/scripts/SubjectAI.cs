using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SubjectAI : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public  NavMeshAgent agent;
    JumpAI jumpAI;
    public float walkSpd = 1.5f;
    public Transform[] patrolPoint;
    float distance;
    float sightRange = 20;
    float currentSpd;
    float AttackRange = 2.5f;
    public bool isAttack = false;
    public Collider lCol;
    public Collider rCol;
    public bool isDead = false;


    enum AIstate { Patrol, Chasing, Attack }
    AIstate currentState = AIstate.Patrol;

    private void Awake()
    {
        patrolPoint = new Transform[6];
        jumpAI = GetComponent<JumpAI>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = Camera.main.GetComponentInParent<Transform>();
        ParoltPointAarry patrolPoints = FindAnyObjectByType<ParoltPointAarry>().gameObject.GetComponent<ParoltPointAarry>();
        for (int i = 0; i < patrolPoint.Length; i++)
        {
            patrolPoint[i] = patrolPoints.patrolPointAarry[i].transform;
        }

        //agent.SetDestination(patrolPoint[Random.Range(0, patrolPoint.Length)].position);
        currentState = AIstate.Patrol;


    }

    public bool enemyStart = false;
    private void Update()
    {
        if (enemyStart&& !jumpAI.isJumped)
            Enemy_Update();
    }

    // Update is called once per frame
    void Enemy_Update()
    {
        if (isDead) return;
        distance = Vector3.Distance(this.transform.position, player.position);

        switch (currentState)
        {
            case AIstate.Patrol:
                FindingPlayer(); break;
            case AIstate.Chasing:
                ChasingPlayer(); break;
            case AIstate.Attack:
                AttackPlayer(); break;
            default: break;

        }

        void FindingPlayer()
        {
            walkSpd = 1.5f;
            agent.speed = walkSpd;
            if (distance < sightRange)
            {
                currentState = AIstate.Chasing;
                return;
            }

            if(!agent.pathPending && agent.remainingDistance <1)
                agent.SetDestination(patrolPoint[Random.Range(0, patrolPoint.Length)].position);

        }

        void ChasingPlayer()
        {

           
                agent.isStopped = false;
                walkSpd = 4f;
                agent.speed = walkSpd;

                agent.SetDestination(player.position);
                if (distance < AttackRange)
                {

                    currentState = AIstate.Attack;

                    return;
                }

                if (distance > sightRange)
                {
                    currentState = AIstate.Patrol;
                    return;
                }
               
                Vector3 forward = transform.forward;
           



        }

        void AttackPlayer()
        {

            //애니메이션 실행
            
            Vector3 directiontoTarget = player.transform.position - transform.position;
            directiontoTarget.y = 0;
            Vector3 enemyRight = transform.right;
            float dotProduct = Vector3.Dot(enemyRight, directiontoTarget.normalized);
            string triggerName;
            if (dotProduct > 0.05)
            {

                triggerName = "isAttackR";
                rCol.gameObject.SetActive(true);
            }
            else if (dotProduct < -0.05)
            {
                triggerName = "isAttackL";
                lCol.gameObject.SetActive(true);
            }
            else
            {
                triggerName = "isAttackF";
                rCol.gameObject.SetActive(true);
                lCol.gameObject.SetActive(true);
            }


            if (!isAttack)
            {
                animator.SetTrigger(triggerName);
                StartCoroutine(AttackDelay());
            }
        }
    }

    IEnumerator AttackDelay()
    {

        float time = 0;
        isAttack = true;
        while (time <= 0.5f)
        {
            agent.isStopped = true;
            time += Time.deltaTime;
            yield return null;

        }
       // agent.isStopped = false;
        isAttack = false;
        currentState = AIstate.Chasing;
    }

    public void RagdollStart()
    {
        isDead = true;
        animator.enabled = false;
        agent.enabled = false;
    }
}
