using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FollowMouseFog : MonoBehaviour
{
    [SerializeField] private VisualEffect effect;
    [SerializeField] private List<VFXExposedProperty> propertyList;
    private void Start()
    {
        effect.visualEffectAsset.GetExposedProperties(propertyList);
        
        propertyList = new List<VFXExposedProperty>();

        foreach (var property in propertyList)
        {
            Debug.Log(property);
        }

    }
    void Update()
    {
        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
