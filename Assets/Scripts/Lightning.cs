using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Lightning : Singleton<Lightning>
{
    public Ease ease;
    public AudioSource audioData;
    public SpriteMask mask;

    //Make sure tween finishes before starting a new tween
    public void Flash()
    {
        mask.enabled = true;
        audioData.Play();
        Debug.Log("Flash");
        DOTween.To(()=> GetComponent<Light2D>().intensity, x => GetComponent<Light2D>().intensity = x, 2f, 0.1f).SetEase(ease).SetLoops(4, LoopType.Yoyo).OnComplete(() => { mask.enabled = false; });
    }
}
