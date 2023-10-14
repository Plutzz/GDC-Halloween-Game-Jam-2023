using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FollowMouseFog : MonoBehaviour
{
    [SerializeField] private VisualEffect effect;
    void Update()
    {
        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        effect.SetVector3("Sphere_transform_position", new Vector3(_cursorPos.x, _cursorPos.y, 0));
    }
}
