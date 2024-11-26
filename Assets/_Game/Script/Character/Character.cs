using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public class Character : GameUnit
{
    [SerializeField] protected float attackDuration = 1f;
    [SerializeField] protected Transform posAttack;
    [SerializeField] protected Transform skin;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Transform headPos;
    [SerializeField] protected SkinnedMeshRenderer skinned;
    [SerializeField] protected LayerMask layerEnemy;
    [SerializeField] protected LayerMask groundLayer;

    public int score;
    public bool isDead;
    public float rangeAttack;
    public Weapon weapon;
    public Animator anim;
    public Transform posWeaponSpawn;
    public GameObject rangePre;
    public WeaponType weaponType;

    protected bool isAttacking;
    protected float shootDelay = 1f;
    protected float shootTime = 0f;
    protected HatData hatData;
    protected PantData pantData;
    protected WeaponData weaponData;

    private int numOfEnemy;
    private string currentAnim;
    private Collider[] hitColliders = new Collider[20];
    private GameObject currentWeapon;
    private GameObject weaponPre;
    private GameObject roundAttack;
    private Weapon weponSpawn;
    private Hat hatSpawn;
    private Vector3 currentDir;

    public Weapon WeponSpawn { get => weponSpawn; set => weponSpawn = value; }
    public Transform FirePos { get => firePos; set => firePos = value; }
    public Hat HatSpawn { get => hatSpawn; set => hatSpawn = value; }

    private void Start()
    {
        OnInit();
        roundAttack = Instantiate(rangePre, transform);
        skin.rotation = Quaternion.identity;
    }
    private void OnEnable()
    {
        int newLayer = LayerMask.NameToLayer(Constants.LAYER_ENEMY);
        gameObject.layer = newLayer;
        rangeAttack = 1.5f;
        score = 0;
    }
    public virtual void OnInit()
    {
        currentAnim = Constants.ANIM_IDEL;
        isAttacking = false;
        int newLayer = LayerMask.NameToLayer(Constants.LAYER_ENEMY);
        gameObject.layer = newLayer;
    }
    protected virtual void ChangeWeapon(WeaponType weaponType)
    {
        weaponData = DataManager.Instance.listWeaponData[(int)weaponType];
        if (WeponSpawn != null)
        {
            Destroy(WeponSpawn.gameObject);
        }
        WeponSpawn = Instantiate(weaponData.weapon, firePos);
    }
    public void ChangeHat(HatType hatType)
    {
        hatData = DataManager.Instance.listHatData[(int)hatType];
        if (hatSpawn != null)
        {
            Destroy(hatSpawn.gameObject);
        }
        hatSpawn = Instantiate(hatData.hat, headPos);
    }
    public void ChangePant(PantType pantType)
    {
        skinned.material = DataManager.Instance.pantDataSO.listPantData[(int)pantType].material;
    }
    protected virtual void Attack()
    {
        if (isAttacking) return;
        Vector3 directionToEnemy = FindTarget(transform.position, rangeAttack);
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
        Quaternion yRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = yRotation;
        shootTime += Time.deltaTime;
        if (shootTime < shootDelay) return;
        shootTime = 0;
        if (directionToEnemy != Vector3.zero)
        {
            ChangeAnim(Constants.ANIM_ATTACK);
            StartCoroutine(PerformAttack(directionToEnemy));
        }
    }
    private IEnumerator PerformAttack(Vector3 directionToEnemy)
    {
        isAttacking = true;
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }
        weapon.shoot(posAttack, directionToEnemy);

        yield return new WaitForSeconds(attackDuration);

        if (currentWeapon != null)
        {
            currentWeapon.SetActive(true);
        }
        isAttacking = false;
    }

    public Vector3 FindTarget(Vector3 position, float radius)
    {
        numOfEnemy = Physics.OverlapSphereNonAlloc(position, radius, hitColliders, layerEnemy);
        float minDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        for (int i = 0; i < numOfEnemy; i++)
        {
            if (hitColliders[i].gameObject == gameObject) continue;
            float distance = Vector3.Distance(position, hitColliders[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = hitColliders[i];
            }
        }
        if (nearestEnemy != null) return (nearestEnemy.transform.position - position).normalized;
        return Vector3.zero;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    public void UpSize()
    {
        rangeAttack += 0.2f;
        roundAttack.transform.localScale *= 1.1f;
        score += 1;
    }
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.15f;
        }
        return TF.position;
    }

    protected virtual void Die()
    {
        ChangeAnim(Constants.ANIM_DEAD);
        int newLayer = LayerMask.NameToLayer(Constants.LAYER_WATER);
        gameObject.layer = newLayer;

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }
}
