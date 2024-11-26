using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO weaponDataSO;
    public List<WeaponData> listWeaponData;

    public HatDataSO hatDataSO;
    public List<HatData> listHatData;

    public PantDataSO pantDataSO;
    public List<PantData> listPantData;

    private PlayerData playerData;

    private void Awake()
    {
        listWeaponData = weaponDataSO.listWeaponData;
        listHatData = hatDataSO.listHatData;
        listPantData = pantDataSO.listPantData;
    }
    public void Init()
    {
        if (!PlayerPrefs.HasKey(Constants.PLAYERPREF_KEY))
        {
            CreatePlayerData();
        }

        playerData = LoadPlayerData();
    }
    public int CoinData { get => playerData.coin; set => playerData.coin = value; }
    public ObjectType GetBulletType(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Axe:
                return ObjectType.Axe;
            case WeaponType.Candy:
                return ObjectType.Candy;
            // Thêm các loại vũ khí khác nếu cần
            default:
                return ObjectType.Axe; // Một loại mặc định nếu cần
        }
    }
    public PlayerData GetPlayerData()
    {
        return playerData;
    }
    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        List<WeaponData> weapons = weaponDataSO.listWeaponData;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponType == weapons[i].weaponType)
            {
                return weapons[i];
            }
        }
        return null;
    }
    public HatData GetHatData()
    {
        List<HatData> hats = hatDataSO.listHatData;
        for (int i = 0; i < hats.Count; i++)
        {
            if (playerData.hatTypeData == hats[i].hatType)
            {
                return hats[i];
            }
        }
        return null;
    }
    public PantData GetPantData()
    {
        List<PantData> pants = pantDataSO.listPantData;
        for (int i = 0; i < pants.Count; i++)
        {
            if (playerData.pantTypeData == pants[i].pantType)
            {
                return pants[i];
            }
        }
        return null;
    }
    public void SaveCoinPlayerData(int coin)
    {
        playerData.coin = coin;
        SavePlayerData(playerData);
    }
    public void SaveWeaponPlayerData(WeaponType weaponType)
    {
        playerData.weaponTypeData = weaponType;
        SavePlayerData(playerData);
    }
    public void SaveHatPlayerData(HatType hatType)
    {
        playerData.hatTypeData = hatType;
        SavePlayerData(playerData);
    }
    public void SavePantPlayerData(PantType pantType)
    {
        playerData.pantTypeData = pantType;
        SavePlayerData(playerData);
    }
    public void SavePlayerData(PlayerData playerData) { 
        string dataString  = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(Constants.PLAYERPREF_KEY, dataString);
    }

    public PlayerData LoadPlayerData() {
        string dataString = PlayerPrefs.GetString(Constants.PLAYERPREF_KEY);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(dataString);
        return playerData;
    }

    public void CreatePlayerData() { 
        playerData = new PlayerData();
        SavePlayerData(playerData);
    }
}
