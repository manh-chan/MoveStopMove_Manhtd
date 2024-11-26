using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Enemys enemys)
    {
        enemys.ChangeAnim(Constants.ANIM_RUN);
        enemys.InvokeRepeating(nameof(enemys.EnemyMove), 0f, enemys.randomMoveInterval);
    }
    public void OnExecute(Enemys enemys)
    {
        
        Vector3 directionToEnemy = enemys.FindTarget(enemys.transform.position, enemys.rangeAttack);
        if (directionToEnemy != Vector3.zero)
        {
            enemys.ChangeSate(new AttackState());
        }
    }

    public void OnExit(Enemys enemys)
    {
     
    }
}
