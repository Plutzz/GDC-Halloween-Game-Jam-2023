using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonFlower : Singleton<UpgradeButton>
{
    public void UpgradeClick()
    {
        Flower.Upgrade();
    }
}
