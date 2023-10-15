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
    private SpriteMask mask;

    void Start()
    {
        mask = GetComponent<SpriteMask>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            audiodata.Play();
            Flash();
        }
    }

    //Make sure tween finishes before starting a new tween
    public void Flash()
    {
        mask.enabled = true;
        Debug.Log("Flash");
        DOTween.To(()=> GetComponent<Light2D>().intensity, x => GetComponent<Light2D>().intensity = x, 2f, 0.1f).SetEase(ease).SetLoops(4, LoopType.Yoyo);
        mask.enabled = false;
    }
}
