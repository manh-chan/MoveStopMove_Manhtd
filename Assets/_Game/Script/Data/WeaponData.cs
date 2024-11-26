using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum WeaponType
    {
        Axe = 0,
        Candy = 1
      //Hammer = 2,
      //Knife = 3,

}
    [Serializable]
    public class WeaponData
    {
        public WeaponType weaponType;
        public Sprite weaponSprite;
        public Bullet bullet;
        public Weapon weapon;
        public int Price;
}
