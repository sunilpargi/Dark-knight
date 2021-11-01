using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyCOntrol : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE, WALK, RUN, PAUSE, GOBACK, ATTACK, DEATH

    }
    private float attack_distance = 1.5f;
    private float alert_Attack_Distance = 8f;
    private float followDistance = 15f;
    private float enemyToPlayerDis;
    private Transform playerTarget;
    private Vector3 initialPosition;

    [HideInInspector]
    public EnemyState enemy_CurrentState = EnemyState.IDLE;
    public EnemyState enemy_LastState = EnemyState.IDLE;

    private float move_Speed = 2f;
    private float walk_Speed = 1f;

    private CharacterController charController;
    private Vector3 WhereTo_Move = Vector3.zero;

    private float CurrentAttackTime;
    private float WaitAttackTime = 1f;

    private Animator anim;
    private bool finished_Animation = true;
    private bool finished_Movement = true;

    private NavMeshAgent navAgent;
    private Vector3 WhereTo_Navigate;

    private EnemyHealth enemyHealth;


    // Start is called before the first frame update
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        initialPosition = transform.position;
        WhereTo_Navigate = transform.position;

        enemyHealth = GetComponent<EnemyHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.health <= 0)
        {
            enemy_CurrentState = EnemyState.DEATH;
        }

        if(enemy_CurrentState !=EnemyState.DEATH)
        {
            enemy_CurrentState = SetEnemyState(enemy_CurrentState,enemy_LastState,enemyToPlayerDis);

            if(finished_Movement)
            {
                GetStateControl(enemy_CurrentState);
            }
            else
            {
                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    finished_Movement = true;
                }
                else if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk1")
                    || anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk2"))
                {
                    anim.SetInteger("Atk", 0);
                } 
            }
        }
        else
        {
            anim.SetBool("Death", true);
            charController.enabled = false;
            navAgent.enabled = false;

            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death")
                && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject, 2f);
            }
        }
    }

    EnemyState SetEnemyState(EnemyState curState, EnemyState lastState, float enemyToPlayerDis)
    {
        enemyToPlayerDis = Vector3.Distance(transform.position, playerTarget.position);

        float initialDistance = Vector3.Distance(initialPosition, transform.position);

        if (initialDistance > followDistance)
        {
            lastState = curState;
            curState = EnemyState.GOBACK;
        }
        else if(enemyToPlayerDis <= attack_distance)
        {
            lastState = curState;
            curState = EnemyState.ATTACK;
        }
        else if(enemyToPlayerDis >= alert_Attack_Distance
            && lastState == EnemyState.PAUSE || lastState == EnemyState.ATTACK)
        {
            lastState = curState;
            curState = EnemyState.PAUSE;
        }
        else if(enemyToPlayerDis <= alert_Attack_Distance && enemyToPlayerDis > attack_distance)
        {
           if(curState != EnemyState.GOBACK || lastState == EnemyState.WALK)
            {
                lastState = curState;
                curState = EnemyState.PAUSE;
            }
        }
        else if(enemyToPlayerDis > alert_Attack_Distance
            && lastState != EnemyState.GOBACK && lastState != EnemyState.PAUSE)
        {
            lastState = curState;
            curState = EnemyState.WALK;
        }
        return curState;
    }

    void GetStateControl(EnemyState curState)
    {
         if(curState == EnemyState.RUN || curState == EnemyState.PAUSE)
        {
            if(curState != EnemyState.ATTACK)
            {
                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                if (Vector3.Distance(transform.position, targetPosition) >= 2f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", true);

                    navAgent.SetDestination(targetPosition);
                }

              
            }
        }
         else if(curState == EnemyState.ATTACK)
        {
            anim.SetBool("Run", false);
            WhereTo_Move.Set(0f, 0f, 0f);

            navAgent.SetDestination(transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(playerTarget.position - transform.position), 5f * Time.deltaTime);

            if(CurrentAttackTime > WaitAttackTime)
            {
                int atkRange = Random.Range(1, 3);

                anim.SetInteger("Atk", atkRange);
                finished_Animation = false;
                CurrentAttackTime = 0f;
            }
            else
            {
                anim.SetInteger("Atk", 0);
                CurrentAttackTime += Time.deltaTime;
            }
        }

         else if(curState == EnemyState.GOBACK)
        {
            anim.SetBool("Run", true);
            Vector3 targetPosition = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);

            navAgent.SetDestination(targetPosition);

            if(Vector3.Distance(targetPosition,initialPosition) <= 3.5f)
            {
                enemy_LastState = curState;
                curState = EnemyState.WALK;
            }
        }
         else if(curState == EnemyState.WALK)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);

            if(Vector3.Distance(transform.position, WhereTo_Navigate) <= 2f)
            {
                WhereTo_Navigate.x = Random.Range(initialPosition.x - 5f, initialPosition.x + 5f);
                WhereTo_Navigate.z = Random.Range(initialPosition.z - 5f, initialPosition.z + 5f);
            }
            else
            {
                navAgent.SetDestination(WhereTo_Navigate);
            }
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
            WhereTo_Move.Set(0f, 0f, 0f);
            navAgent.isStopped = true;
        }
        charController.Move(WhereTo_Move);
    }
}







