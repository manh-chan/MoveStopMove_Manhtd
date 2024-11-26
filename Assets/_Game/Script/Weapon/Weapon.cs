using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [HideInInspector]public Bullet bullet;
    [HideInInspector]public Character character;
    private void Start()
    {
        character = GetComponent<Character>();
    }
    public virtual void shoot(Transform posShoot, Vector3 direction)
    {
        ObjectType bulletType = DataManager.Instance.GetBulletType(character.weaponType);
        bullet = SimplePool.Spawn<Bullet>(bulletType, posShoot.position,Quaternion.Euler(90,0,0));
        bullet.SetDirection(direction);
        bullet.SetUsingPeopel(character);
    }
}