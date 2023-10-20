using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class Flower : BaseTurret
{
    [SerializeField] private Transform[] firingPoints;
    private static float rotationSpeedBase = 500f;
    private static float rotationUpgradeFactor = 0.3f;
    protected override void Start()
    {
        base.Start();
        CalculateAttributes();
        //SET BASE CANNON VALUES
        //---------------------------

        // Cost
        baseUpgradeCost = 500;
        upgradeCostFactor = 0.8f;

        // Bullets Per Second
        bpsBase = 1f;
        bpsUpgradeFactor = 0.6f;

        // Damage
        damageBase = 5;
        damageUpgradeFactor = 1;
        
        // Rotation Speed
        rotationSpeedBase = 500f;
        rotationUpgradeFactor = 0.3f;

        // Range
        targetingRangeBase = 5f;
        targetingRangeUpgradeFactor = 0.6f;

        // Other Varibles
        lifetime = 20;
        
    }

    protected override void Shoot()
    {
        foreach (var firingPoint in firingPoints)
        {
            GameObject _bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            FlowerBullet bulletScript = _bullet.GetComponent<FlowerBullet>();
            bulletScript.SetDamage(damage);
            bulletScript.SetTarget(target);
        }
    }

    protected override void CalculateAttributes()
    {
        // DO NOT USE base.CalculateAttributes if we don't want range to upgrade with flower turret level
        base.CalculateAttributes();
        rotationSpeed = CalculateRotationSpeed();
    }

    private float CalculateRotationSpeed()
    {
        return rotationSpeedBase * Mathf.Pow(level, rotationUpgradeFactor);
    }

    protected override void RotateTowardsTarget()
    {
        turretRotationPoint.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
}


