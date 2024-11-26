using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private PlayerData playerData;

    [Header("WEAPON")]
    public ShopTemplate[] shopWeaponTemplate;
    public GameObject[] shopWeaponSO;
    public Button[] weaponBuyBtns;
    public Button[] weaponEquipBtns;

    [Header("HAT")]
    public ShopTemplate[] shopHatTemplate;
    public GameObject[] shopHatSO;
    public Button[] hatBuyBtns;
    public Button[] hatEquipBtns;

    [Header("PANT")]
    public ShopTemplate[] shopPantTemplate;
    public GameObject[] shopPantSO;
    public Button[] pantBuyBtns;
    public Button[] pantEquipBtns;
    private void Awake()
    {
        playerData = DataManager.Instance.GetPlayerData();
    }
    public void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        for (int i = 0; i < DataManager.Instance.listWeaponData.Count; i++)
        {
            shopWeaponSO[i].SetActive(true);
        }
        for (int i = 0; i < DataManager.Instance.listHatData.Count; i++)
        {
            shopHatSO[i].SetActive(true);
        }
        for (int i = 0; i < DataManager.Instance.listPantData.Count; i++)
        {
            shopPantSO[i].SetActive(true);
        }
        LoadPanelHat();
        LoadPanelWeapon();
        LoadPanelPant();
        CheckWeaponPurchaseable();
        CheckHatPurchaseable();
        CheckPantPurchaseable();
    }
    // Weapon Shop
    private void CheckWeaponPurchaseable()
    {
        var listWeaponData = DataManager.Instance.listWeaponData;
        for (int i = 0; i < DataManager.Instance.listWeaponData.Count; i++)
        {
            if (playerData.coin >= listWeaponData[i].Price) weaponBuyBtns[i].interactable = true;
            else weaponBuyBtns[i].interactable = false;
        }
    }

    public void PurchaseItemWeapon(int btnNo)
    {
        var listWeaponData = DataManager.Instance.listWeaponData;
        if (playerData.coin >= listWeaponData[btnNo].Price)
        {
            playerData.coin = playerData.coin - listWeaponData[btnNo].Price;
            CheckWeaponPurchaseable();
        }
    }

    private void LoadPanelWeapon()
    {
        var listWeaponData = DataManager.Instance.listWeaponData;
        for (int i = 0; i < DataManager.Instance.listWeaponData.Count; i++)
        {
            shopWeaponTemplate[i].titleTxt.text = listWeaponData[i].weaponType.ToString();
            shopWeaponTemplate[i].costTxt.text = listWeaponData[i].Price.ToString();
            shopWeaponTemplate[i].itemImg.sprite = listWeaponData[i].weaponSprite;
        }
    }

    public void PressEquipWeaponShop(int btnNo)
    {
        var listWeaponData = DataManager.Instance.listWeaponData;
        Destroy(LevelManager.Instance.player.WeponSpawn.gameObject);
        LevelManager.Instance.player.EquipWeapon(listWeaponData[btnNo].weaponType);
    }

    // Hat Shop
    private void CheckHatPurchaseable()
    {
        var listHatData = DataManager.Instance.listHatData;
        for (int i = 0; i < DataManager.Instance.listHatData.Count; i++)
        {
            if (playerData.coin >= listHatData[i].price) hatBuyBtns[i].interactable = true;
            else hatBuyBtns[i].interactable = false;
        }
    }
    public void PurchaseItemHat(int btnNo)
    {
        var listHatData = DataManager.Instance.listHatData;
        if (playerData.coin >= listHatData[btnNo].price)
        {
            playerData.coin = playerData.coin - listHatData[btnNo].price;
            CheckHatPurchaseable();
        }
    }
    private void LoadPanelHat()
    {
        var listHatData = DataManager.Instance.listHatData;
        for (int i = 0; i < DataManager.Instance.listHatData.Count; i++)
        {
            shopHatTemplate[i].titleTxt.text = listHatData[i].hatType.ToString();
            shopHatTemplate[i].costTxt.text = listHatData[i].price.ToString();
            shopHatTemplate[i].itemImg.sprite = listHatData[i].hatSprite;
        }

    }
    public void PressEquipHatShop(int btnNo)
    {
        var listHatData = DataManager.Instance.listHatData;
        Destroy(LevelManager.Instance.player.HatSpawn.gameObject);
        LevelManager.Instance.player.EquipHat(listHatData[btnNo].hatType);
    }

    //Pant shop
    private void CheckPantPurchaseable()
    {
        var listPantData = DataManager.Instance.listPantData;
        for (int i = 0; i < DataManager.Instance.listHatData.Count; i++)
        {
            if (playerData.coin >= listPantData[i].price) pantBuyBtns[i].interactable = true;
            else pantBuyBtns[i].interactable = false;
        }
    }
    public void PurchaseItemPant(int btnNo)
    {
        var listPantData = DataManager.Instance.listPantData;
        if (playerData.coin >= listPantData[btnNo].price)
        {
            playerData.coin = playerData.coin - listPantData[btnNo].price;
            CheckPantPurchaseable();
        }
    }
    private void LoadPanelPant()
    {
        var listPantData = DataManager.Instance.listPantData;
        for (int i = 0; i < DataManager.Instance.listHatData.Count; i++)
        {
            shopPantTemplate[i].titleTxt.text = listPantData[i].pantType.ToString();
            shopPantTemplate[i].costTxt.text = listPantData[i].price.ToString();
            shopPantTemplate[i].itemImg.sprite = listPantData[i].pantSprite;
        }
    }
    public void PressEquipPantShop(int btnNo)
    {
        var listPantData = DataManager.Instance.listPantData;
        LevelManager.Instance.player.EquipPant(listPantData[btnNo].pantType);
    }
}
