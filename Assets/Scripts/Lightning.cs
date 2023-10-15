using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Lightning : MonoBehaviour
{
    public Ease ease;
    public AudioSource audiodata;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audiodata.Play();
            Flash();
        }
    }

    //Make sure tween finishes before starting a new tween
    public void Flash()
    {
        Debug.Log("Flash");
        DOTween.To(()=> GetComponent<Light2D>().intensity, x => GetComponent<Light2D>().intensity = x, 2f, 0.1f).SetEase(ease).SetLoops(4, LoopType.Yoyo);
    }
}
