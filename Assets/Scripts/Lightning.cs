using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Lightning : Singleton<Lightning>
{
    public SpriteMask mask;

    //Make sure tween finishes before starting a new tween
    public void Flash()
    {
        mask.enabled = true;
        SoundManager.Instance.PlayEntireSound(SoundManager.Sounds.Thunder);
        Debug.Log("Flash");
        DOTween.To(()=> GetComponent<Light2D>().intensity, x => GetComponent<Light2D>().intensity = x, 2f, 0.1f).SetLoops(4, LoopType.Yoyo).OnComplete(() => { mask.enabled = false; });
    }
}
