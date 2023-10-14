using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;

    [SerializeField] Ease menuOpenEase;
    [SerializeField] float menuOpenTime = 1f;
    private bool menuOpen = true;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.Instance.Currency.ToString();
    }

    public void SetSelected()
    {

    }

    public void OpenMenu()
    {
        if(menuOpen == true)
        {
            GetComponent<RectTransform>().DOAnchorPosX(150f, menuOpenTime).SetEase(menuOpenEase);
            menuOpen = false;
        }
        else
        {
            GetComponent<RectTransform>().DOAnchorPosX(-150f, menuOpenTime).SetEase(menuOpenEase);
            menuOpen = true;
        }
        
    }

}
