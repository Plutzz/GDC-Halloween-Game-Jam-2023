using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : Singleton<UpgradeButton>
{
    public void UpgradeClick()
    {
        Cannon.Upgrade();
    }
}
