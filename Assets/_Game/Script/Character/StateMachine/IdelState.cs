using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : IState
{
    private float time,timeDelay;
    public void OnEnter(Enemys enemys)
    {
      enemys.ChangeAnim(Constants.ANIM_IDEL);
      time = 0;
      timeDelay = Random.Range(0f,1f);//serialized
    }

    public void OnExecute(Enemys enemys)
    {
        time += Time.deltaTime;
        if (time > timeDelay) enemys.ChangeSate(new PatrolState());
    }

    public void OnExit(Enemys enemys)
    {

    }
}
