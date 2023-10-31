using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsnapManager : BaseTurretManager
{
    public override void Upgrade()
    {
        base.Upgrade();
        Debug.Log("Seedsnap Upgrade");
    }
}
