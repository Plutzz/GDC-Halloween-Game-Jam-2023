using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonSniper : Singleton<UpgradeButton>
{
    public void UpgradeClick()
    {
        Sniper.Upgrade();
    }
}
