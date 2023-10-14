using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _cursorPos;
    }
}
