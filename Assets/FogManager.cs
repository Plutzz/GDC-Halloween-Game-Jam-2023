using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem fog;
    [SerializeField] private ParticleSystem.MinMaxGradient gradient;
    [SerializeField] private ParticleSystem.MinMaxGradient gradient2;

    private void Update()
    {
        ParticleSystem.ColorOverLifetimeModule colorModule = fog.colorOverLifetime;
        colorModule.color = gradient;
    }
}
