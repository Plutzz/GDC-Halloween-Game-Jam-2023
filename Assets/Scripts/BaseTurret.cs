using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    public static float lifetime { get; private set; } = 20f;
    protected static float rotationSpeed = 500f;

    //Cost variables
    protected static float upgradeCostFactor = 0.8f;
    protected static int baseUpgradeCost = 10000;

    public static int level { get; private set; } = 1;
    public static int maxLevel { get; private set; } = 3;

    protected Transform target;
    protected float timeUntilFire;

    protected float timeAlive;



    public bool isActive;
    public Image timerBar;



    protected virtual void Start()
    {
        isActive = false;
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


    private void OnMouseEnter()
    {
        CustomCursor.Instance.setCursor(1);
    }

    private void OnMouseExit()
    {
        CustomCursor.Instance.setCursor(0);
    }
}
