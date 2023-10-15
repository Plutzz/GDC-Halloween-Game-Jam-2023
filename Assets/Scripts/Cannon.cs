using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using Unity.VisualScripting;

public class Cannon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject rangeDisplay;



    //Range that turret can target
    public static float targetingRange { get; private set; }
    private static float targetingRangeBase = 5f;
    private static float targetingRangeUpgradeFactor = 0.4f;

    //Bullets per second
    public static float bps {get; private set;}                             
    private static float bpsBase = 1f; 
    private static float bpsUpgradeFactor = 0.6f;

    //Damage Per Bullet
    public static int damage {get; private set;}
    private static int damageBase = 5;
    private static float damageUpgradeFactor = 0.3f;

    //Misc Stats
    public static float lifetime { get; private set; } = 60f;
    private static float rotationSpeed = 500f;

    //Cost variables
    private static float upgradeCostFactor = 0.8f;
    private static int baseUpgradeCost = 100;

    public bool isActive = false;                                    //Prevents preview turret from firing
    private Transform target;
    private float timeUntilFire;
    private float timeAlive;

    public static int level { get; private set; } = 1;

    private void Start()
    {
        isActive = false;

        CalculateAttributes();

        rangeDisplay.transform.localScale = new Vector3(targetingRange * 2, targetingRange * 2, 1f);
    }

    private void Update()
    {
        if (!isActive) return;

        timeUntilFire += Time.deltaTime;

        timeAlive += Time.deltaTime;

        if (!(lifetime < 0) && timeAlive > lifetime)   
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!UIManager.Instance.IsHoveringUI())
            {
                rangeDisplay.SetActive(false);
            }

        }

        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {

            if(timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        TurretBullet bulletScript = _bullet.GetComponent<TurretBullet>();
        bulletScript.SetDamage(damage);
        bulletScript.SetTarget(target);
    }

    

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemies);

        if(hits.Length > 0 )
        {
            target = hits[0].transform;
        }

        
    }

    private bool CheckTargetIsInRange()
    {
        return (Vector2.Distance(target.position, transform.position) <= targetingRange);
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion _targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void DisplayRange()
    {
        rangeDisplay.transform.localScale = new Vector3(targetingRange * 2, targetingRange * 2, 1f);
        upgradeUI.SetActive(true);
    }

    public void DisableDisplayRange()
    {
        upgradeUI.SetActive(false);
    }

    public static void Upgrade()
    {
        if (CalculateCost() > LevelManager.Instance.Currency) return;

        LevelManager.Instance.SpendCurrency(CalculateCost());

        Debug.Log("Upgrade");

        level++;


        // if num turrets > 0, call onUpgrade (prevents error)
        CalculateAttributes();
    }

    public static int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, upgradeCostFactor));
    }

    private static int CalculateDamage()
    {
        return Mathf.RoundToInt(damageBase * Mathf.Pow(level, damageUpgradeFactor));
    }

    private static float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, bpsUpgradeFactor);
    }
    private static float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, targetingRangeUpgradeFactor);
    }
    private static void CalculateAttributes()
    {
        bps = CalculateBPS();
        targetingRange = CalculateRange();
        damage = CalculateDamage();

        //CloseUpgradeUI(); //Use this if you want UI to close after every upgrade
        Debug.Log("New BPS: " + bps);
        Debug.Log("New Range: " + targetingRange);
        Debug.Log("New Cost: " + CalculateCost());
        Debug.Log("New Damage: " + CalculateDamage());
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

   private void OnMouseEnter()
    {
        CustomCursor.Instance.setCursor(1);
    }

    private void OnMouseExit()
    {
        CustomCursor.Instance.setCursor(0);
    }

    private void OnMouseOver()
    {
        //TODO: Check if not in build mode
        if (Input.GetMouseButton(0))
        {
            if(!UIManager.Instance.IsHoveringUI())
            {
                DisplayRange();
            }
        }
    }
}
