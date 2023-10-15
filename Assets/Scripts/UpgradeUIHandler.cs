using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.SetHoveringState(false);
        Debug.Log("Left UI");
    }
}
