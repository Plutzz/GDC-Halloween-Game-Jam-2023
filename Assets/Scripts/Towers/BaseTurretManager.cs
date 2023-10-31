using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretManager : Singleton<BaseTurretManager>
{
    [Header("Passed Stats")]
    [SerializeField] protected TurretStats stats;

    [Header("Base Stats")]
    [Header("Range")]
    [SerializeField] protected float targetingRangeBase = 5f;
    [SerializeField] protected float targetingRangeUpgradeFactor = 0.6f;
    [Header("Bullets Per Second")]
    [SerializeField] protected float bpsBase = 1f;
    [SerializeField] protected float bpsUpgradeFactor = 0.6f;
    [Header("Damage")]
    [SerializeField] protected int damageBase = 5;
    [SerializeField] protected float damageUpgradeFactor = 0.9f;
    
    [Header("Cost")]
    [SerializeField] protected int baseUpgradeCost = 1000;
    [SerializeField] protected float upgradeCostFactor = 0.8f;

    [Header("Levels")]
    public int maxLevel = 3;
    public int level { get; protected set; }

    public event Action onUpgrade;

    protected override void Awake()
    {
        base.Awake();
        CalculateStats();
        level = 1;
    }
    public virtual void Upgrade()
    {
        if (level == maxLevel) return;
        if (CalculateCost() > LevelManager.Instance.Currency) return;

        LevelManager.Instance.SpendCurrency(CalculateCost());

        level++;

        onUpgrade();

        Debug.Log("BaseTurret upgrade");
    }

    protected int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, upgradeCostFactor));
    }

    protected int CalculateDamage()
    {
        return Mathf.RoundToInt(damageBase * Mathf.Pow(level, damageUpgradeFactor));
    }

    protected float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, bpsUpgradeFactor);
    }
    protected float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, targetingRangeUpgradeFactor);
    }

    // NEED TO MAKE A NEW CALCULATE ATTRIBUTES METHOD IN INHERITED CLASSES
    protected void CalculateStats()
    {
        stats.bps = CalculateBPS();
        stats.targetingRange = CalculateRange();
        stats.damage = CalculateDamage();
    }

    public TurretStats GetStats()
    {
        CalculateStats();
        return stats;
    }

}
