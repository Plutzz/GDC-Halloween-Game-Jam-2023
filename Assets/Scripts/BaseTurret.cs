using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BaseTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected LayerMask enemies;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected GameObject rangeDisplay;
    [SerializeField] protected GameObject graphics;

    [SerializeField] protected Sprite[] levelSprites;



    // STATS
    //--------------------------------------------------
    //Range that turret can target
    public static float targetingRange { get; private set; }
    protected static float targetingRangeBase = 5f;
    protected static float targetingRangeUpgradeFactor = 0.4f;

    //Bullets per second
    public static float bps { get; private set; }
    protected static float bpsBase = 1f;
    protected static float bpsUpgradeFactor = 0.6f;

    //Damage Per Bullet
    public static int damage { get; private set; }
    protected static int damageBase = 5;
    protected static float damageUpgradeFactor = 0.3f;

    //Misc Stats
    public static float lifetime { get; protected set; } = 20f;
    public static float rotationSpeed { get; protected set; } 

    //Cost variables
    protected static float upgradeCostFactor = 0.8f;
    protected static int baseUpgradeCost = 1000;

    public static int level { get; private set; } = 1;
    public static int maxLevel { get; private set; } = 3;

    protected Transform target;
    protected float timeUntilFire;

    protected float timeAlive;

    protected static event Action onUpgrade;

    public bool isActive;
    public Image timerBar;



    protected virtual void Start()
    {
        isActive = false;

        onUpgrade += CalculateAttributes;

        CalculateAttributes();
    }

    protected void Update()
    {
        graphics.GetComponent<SpriteRenderer>().sprite = levelSprites[level - 1];

        if (!isActive) return;

        timeUntilFire += Time.deltaTime;
        timeAlive += Time.deltaTime;


        rangeDisplay.transform.localScale = new Vector3(targetingRange * 2, targetingRange * 2, 1f);
        rangeDisplay.GetComponent<Light2D>().pointLightOuterRadius = targetingRange;

        if (!(lifetime < 0))
        {
            timerBar.fillAmount = 1 - (timeAlive / lifetime);
        }

        // If building has expired destroy building and free up the grid spot
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

    
    //Make a default bullet script
    protected virtual void Shoot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        TurretBullet bulletScript = _bullet.GetComponent<TurretBullet>();
        bulletScript.SetDamage(damage);
        bulletScript.SetTarget(target);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.CannonAttack);
    }

    public static void Upgrade()
    {
        if (level == maxLevel) return;
        if (CalculateCost() > LevelManager.Instance.Currency) return;

        LevelManager.Instance.SpendCurrency(CalculateCost());

        level++;

        onUpgrade();
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

    // NEED TO MAKE A NEW CALCULATE ATTRIBUTES METHOD IN INHERITED CLASSES
    protected virtual void CalculateAttributes()
    {
        bps = CalculateBPS();
        targetingRange = CalculateRange();
        damage = CalculateDamage();
    }

    protected virtual void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion _targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return (Vector2.Distance(target.position, transform.position) <= targetingRange);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemies);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    /*
    private void OnMouseEnter()
    {
        CustomCursor.Instance.setCursor(1);
    }

    private void OnMouseExit()
    {
        CustomCursor.Instance.setCursor(0);
    }
    */
}
