using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemys enemys)
    {
        enemys.EnemyStop();
    }

    public void OnExecute(Enemys enemys)
    {
        if(enemys.isDead) return;
        enemys.EnemyAttack();
        Vector3 directionToEnemy = enemys.FindTarget(enemys.transform.position, enemys.rangeAttack);
        if (directionToEnemy == Vector3.zero)
        {
            enemys.ChangeSate(new PatrolState());
        }
    }

    public void OnExit(Enemys enemys)
    {
       
    }
}
