using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.UIElements;

public class Player : Character
{
    [Header("Player Setting")]
    private float speedMove;
    private int coinPlayer;

    private Vector3 move;
    private HatType hatType;
    private PantType pantType;

    public HatType HatType { get => hatType; set => hatType = value; }
    public int CoinPlayer { get => coinPlayer; set => coinPlayer = value; }

    private void Update()
    {
        if (isDead) return;
        PlayerMove();
        
        if (Input.GetAxis("Fire1") != 1) PlayerAttack();
    }

    public override void OnInit()
    {
        isDead = false;
        coinPlayer = 0;
        speedMove = 2f;
        pantType = DataManager.Instance.GetPlayerData().pantTypeData;
        gameObject.transform.position = Vector3.zero;
        FollowCamera.Instance.OnInit();
        base.OnInit();
        ChangeItems();
        ChangePant(pantType);
    }

    private void PlayerAttack()
    {
        Attack();
    }
    protected void PlayerMove()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPoint = JoystickControl.direct * speedMove * Time.deltaTime + TF.position;
            TF.position = CheckGround(nextPoint);
            if (JoystickControl.direct != Vector3.zero)
            {
                skin.forward = JoystickControl.direct;
            }
            ChangeAnim(Constants.ANIM_RUN);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim(Constants.ANIM_IDEL);
        }
    }
    protected override void Die()
    {
        base.Die();
        GameManager.Instance.UpdateGameState(GameManager.GameState.fail);
    }

    public void EquipWeapon(WeaponType weaponType)
    {
        ChangeWeapon(weaponType);
        DataManager.Instance.SaveWeaponPlayerData(weaponType);
    }
    public void EquipHat(HatType hatType)
    {
        ChangeHat(hatType);
        DataManager.Instance.SaveHatPlayerData(hatType);
    }
    public void EquipPant(PantType pantType)
    {
        ChangePant(pantType);
        DataManager.Instance.SavePantPlayerData(pantType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_BULLET))
        {
            isDead = true;
            Die();
        }
    }
    private void ChangeItems() {
        // Weapon data
        if (weaponData == null)
        {
            weaponType = DataManager.Instance.LoadPlayerData().weaponTypeData;
            weaponData = DataManager.Instance.GetWeaponData(weaponType);

        }
        ChangeWeapon(weaponType);

        //Hat data
        if (hatData == null)
        {
            hatData = DataManager.Instance.GetHatData();
        }
        hatType = DataManager.Instance.GetPlayerData().hatTypeData;
        ChangeHat(hatType);

        //Pant data
        if (pantData == null)
        {
            pantData = DataManager.Instance.GetPantData();
        }
    }
}
