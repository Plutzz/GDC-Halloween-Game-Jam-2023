using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;


public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;


    [Header("Attribute")]
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float targetingRange;
    [SerializeField] private float targetingRangeUpgradeFactor = 0.4f;
    [SerializeField] private float bps;                             //Bullets Per Second
    [SerializeField] private float bpsUpgradeFactor = 0.6f;
    [SerializeField] private float upgradeCostFactor = 0.8f;
    [SerializeField] private int baseUpgradeCost = 100;
    public bool canFire = false;                                    //Prevents preview turret from firing

    private float targetingRangeBase = 5f;
    private float bpsBase = 1f; //Bullets Per Second

    private static Turret selectedTurret;
    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void Start()
    {
        canFire = false;

        bps = bpsBase;
        targetingRange = targetingRangeBase;

        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update()
    {
        if (!canFire) return;

        if(target == null)
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
            timeUntilFire += Time.deltaTime;

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

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.Instance.Currency) return;

        LevelManager.Instance.SpendCurrency(CalculateCost());

        level++;

        bps = CalculateBPS();
        targetingRange = CalculateRange();

        //CloseUpgradeUI(); //Use this if you want UI to close after every upgrade
        Debug.Log("New BPS: " + bps);
        Debug.Log("New Range: " + targetingRange);
        Debug.Log("New Cost: " + CalculateCost());

    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, upgradeCostFactor));
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, bpsUpgradeFactor);
    }
    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, targetingRangeUpgradeFactor);
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
                SelectTurret();
                OpenUpgradeUI();
            }
        }
    }

    private void SelectTurret()
    {
        selectedTurret = this;
    }

}
