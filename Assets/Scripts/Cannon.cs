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
    protected override void Start()
    {
        base.Start();
        CalculateAttributes();
    }

    private void Update()
    {
        graphics.GetComponent<SpriteRenderer>().sprite = levelSprites[level - 1];

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
    private void OnDrawGizmos()
    {
       //Handles.color = Color.cyan;
       //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }


}


