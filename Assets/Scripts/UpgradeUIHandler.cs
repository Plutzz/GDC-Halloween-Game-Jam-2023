using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
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
        mouse_over = true;
        UIManager.Instance.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        UIManager.Instance.SetHoveringState(false);
        Debug.Log("Left UI");
    }
}
