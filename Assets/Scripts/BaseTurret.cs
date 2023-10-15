using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseTurret : MonoBehaviour
{
    public bool isActive;
    public Image timerBar;

    protected virtual void Start()
    {
        isActive = false;
    }

    private void OnMouseEnter()
    {
        CustomCursor.Instance.setCursor(1);
    }

    private void OnMouseExit()
    {
        CustomCursor.Instance.setCursor(0);
    }
}
