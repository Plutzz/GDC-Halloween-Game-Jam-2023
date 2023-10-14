using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private bool isHoveringUI;

    public void SetHoveringState(bool _state)
    {
        isHoveringUI = _state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }
}
