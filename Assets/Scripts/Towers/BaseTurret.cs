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
    [SerializeField] protected Animation[] animations;



    // STATS
    [SerializeField] protected TurretStats stats;

    protected BaseTurretManager manager;
    protected Transform target;
    protected float timeUntilFire;
    protected float timeAlive;
    public bool isActive;
    public Image timerBar;

    protected virtual void Awake()
    {
        isActive = false;
    }

    protected virtual void Start()
    {
        manager = BaseTurretManager.Instance;
        manager.Upgrade();
        // Get stats from manager
    }

    protected void Update()
    {   
        graphics.GetComponent<SpriteRenderer>().sprite = levelSprites[manager.level - 1];

        if (!isActive) return;

        timeUntilFire += Time.deltaTime;
        timeAlive += Time.deltaTime;


        rangeDisplay.transform.localScale = new Vector3(stats.targetingRange * 2, stats.targetingRange * 2, 1f);
        rangeDisplay.GetComponent<Light2D>().pointLightOuterRadius = stats.targetingRange;

        if (!(stats.lifetime < 0))
        {
            timerBar.fillAmount = 1 - (timeAlive / stats.lifetime);
        }

        // If building has expired destroy building and free up the grid spot
        if (!(stats.lifetime < 0) && timeAlive > stats.lifetime)
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
        
        // If target is out of range set target to null
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        // If target is in range check if cooldown is done
        else
        {

            if (timeUntilFire >= 1f / stats.bps)
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
        bulletScript.SetDamage(stats.damage);
        bulletScript.SetTarget(target);

        PlayShootingAnimation();
        SoundManager.Instance.PlaySound(SoundManager.Sounds.CannonAttack);
    }

    protected virtual void PlayShootingAnimation()
    {
        animations[manager.level - 1].Play();
    }
    protected virtual void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion _targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, _targetRotation, stats.rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return (Vector2.Distance(target.position, transform.position) <= stats.targetingRange);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, stats.targetingRange, (Vector2)transform.position, 0f, enemies);

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
