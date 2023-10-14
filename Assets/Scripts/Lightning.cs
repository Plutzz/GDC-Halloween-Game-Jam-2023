using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Lightning : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Flash();
        }
    }

    public void Flash()
    {
        GetComponent<Light2D>().DOIntensity(1000f, 0.25f).SetLoops(1, LoopType.Yoyo);
    }
}
