using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public WeaponType weaponTypeData;
    public HatType hatTypeData;
    public PantType pantTypeData;
    public int coin;

    public List<int> weaponList = new List<int>();
    public List<int> hatList = new List<int>();
    public List<int> pantList = new List<int>();

    public PlayerData() {
        weaponTypeData = WeaponType.Candy;
        hatTypeData = HatType.Arrow;
        pantTypeData = PantType.Batman;
        coin = 0;

        weaponList.Add(0);
        hatList.Add(0);
        pantList.Add(0);
    }

}