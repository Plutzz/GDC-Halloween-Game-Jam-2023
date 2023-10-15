using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public bool isActive;

    protected virtual void Start()
    {
        isActive = false;
    }    
}
