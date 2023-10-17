using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class Cannon : BaseTurret
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject rangeDisplay;
    [SerializeField] private GameObject cannonGraphics;

    [SerializeField] private Sprite[] levelSprites;


    //Range that turret can target
    public static float targetingRange { get; private set; }
    private static float targetingRangeBase = 5f;
    private static float targetingRangeUpgradeFactor = 0.4f;

    //Bullets per second
    public static float bps { get; private set; }
    private static float bpsBase = 1f;
    private static float bpsUpgradeFactor = 0.6f;

    //Damage Per Bullet
    public static int damage { get; private set; }
    private static int damageBase = 5;
    private static float damageUpgradeFactor = 0.3f;

    //Misc Stats
    public static float lifetime { get; private set; } = 20f;
    private static float rotationSpeed = 500f;

    //Cost variables
    private static float upgradeCostFactor = 0.8f;
    private static int baseUpgradeCost = 10000;

    private Transform target;
    private float timeUntilFire;
    private float timeAlive;

    public static int level { get; private set; } = 1;
    public static int maxLevel { get; private set; } = 3;

    protected override void Start()
    {
        base.Start();
        CalculateAttributes();
    }

    private void Update()
    {
        cannonGraphics.GetComponent<SpriteRenderer>().sprite = levelSprites[level - 1];

        if (!isActive) return;

        timeUntilFire += Time.deltaTime;

        
        rangeDisplay.transform.localScale = new Vector3(targetingRange * 2, targetingRange * 2, 1f);
        rangeDisplay.GetComponent<Light2D>().pointLightOuterRadius = targetingRange;

        timeAlive += Time.deltaTime;
        if(!(lifetime < 0))
        {
            timerBar.fillAmount = 1 - (timeAlive / lifetime);
        }

        if (!(lifetime < 0) && timeAlive > lifetime)
        {
            GridBuildingSystem.Instance.destroyBuilding(GetComponent<Building>());
            Destroy(gameObject);
        }

        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {

            if (timeUntilFire >= 1f / bps)
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

        if (hits.Length > 0)
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

    public static void Upgrade()
    {
        if (level == maxLevel) return;
        if (CalculateCost() > LevelManager.Instance.Currency) return;

        LevelManager.Instance.SpendCurrency(CalculateCost());

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
    }

    private void OnDrawGizmos()
    {
       //Handles.color = Color.cyan;
       //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }


}


