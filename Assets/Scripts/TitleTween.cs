using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTween : MonoBehaviour
{
    [SerializeField] private Ease ease;
    void Start()
    {
        GetComponent<RectTransform>().DOAnchorPosY(-13.5f, 2f, true).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
