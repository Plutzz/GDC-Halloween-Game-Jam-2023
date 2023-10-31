using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class Seedsnap : BaseTurret
{
    protected override void Start()
    {

        //base.Start();


        manager = SeedsnapManager.Instance;

        Debug.Log(manager);

        manager.Upgrade();
    }
}


