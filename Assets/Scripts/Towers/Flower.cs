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
        //SET BASE CANNON VALUES
        //---------------------------

        // Cost
    }

    protected override void Shoot()
    {
        foreach (var firingPoint in firingPoints)
        {
            GameObject _bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            FlowerBullet bulletScript = _bullet.GetComponent<FlowerBullet>();
            bulletScript.SetDamage(stats.damage);
            bulletScript.SetTarget(target);
        }
    }

//    private float CalculateRotationSpeed()
  //  {
    //    return rotationSpeedBase * Mathf.Pow(stats.level, rotationUpgradeFactor);
    //}

    protected override void RotateTowardsTarget()
    {
        turretRotationPoint.Rotate(0f, 0f, stats.rotationSpeed * Time.deltaTime, Space.Self);
    }
}


