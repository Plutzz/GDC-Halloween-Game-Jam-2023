using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            if (!UIManager.Instance.IsHoveringUI())
            {
                gameObject.SetActive(false);
            }
           
        }
    }
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
