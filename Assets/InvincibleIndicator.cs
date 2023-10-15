using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleIndicator : Singleton<InvincibleIndicator>
{
    public static InvincibleIndicator Instance;
    public SpriteRenderer spriteRenderer;

    public void FadeCharacter ()
    {
        Color tmp = spriteRenderer.color;
        tmp.a = 0f;
        spriteRenderer.color = tmp;
    }
}
