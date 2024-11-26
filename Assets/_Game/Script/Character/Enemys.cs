using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemys : Character
{
    [Header("Enemy Setting")]
    public float randomMoveInterval = 5f; // Khoảng thời gian giữa các lần di chuyển
    public float randomMoveRadius = 5f; // Bán kính để tìm vị trí ngẫu nhiên
    public NavMeshAgent agent;

    private IState currentState;
    public override void OnInit()
    {
        base.OnInit();
        ChangeWeapon(WeaponType.Axe);        
        isDead = false;
        ChangeSate(new IdelState());
    }
    private void OnEnable()
    {
        agent.enabled = true;
        isDead = false;
        
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void EnemyMove()
    {
        if (!agent.isOnNavMesh) return;
        Vector3 randomPosition = GetRandomPoint(transform.position, randomMoveRadius);
        if (randomPosition != Vector3.zero) agent.SetDestination(randomPosition);
    }
    public void EnemyAttack()
    {
        Attack();
    }

    public void EnemyStop()
    {
        agent.SetDestination(agent.transform.position);
    }
    public void ChangeSate(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    private Vector3 GetRandomPoint(Vector3 origin, float distance)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }
    protected override void Die()
    {
        base.Die();
        ChangeAnim(Constants.ANIM_DEAD);
        isDead = true;
    }
    private void EnemyDeath(){
        LevelManager.Instance.OnEnemyDeath(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_BULLET))
        {
            Die();
            Invoke(nameof(EnemyDeath), 1.5f);
        }
    }

}
