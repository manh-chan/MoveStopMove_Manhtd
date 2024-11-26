using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {
    void OnEnter(Enemys enemys);
    void OnExecute(Enemys enemys);
    void OnExit(Enemys enemys);
}
