using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleIndicator : Singleton<InvincibleIndicator>
{
    public SpriteRenderer spriteRenderer;

    public void FadeCharacter ()
    {
        Color tmp = spriteRenderer.color;
        tmp.a = 0.5f;
        spriteRenderer.color = tmp;
        StartCoroutine(FadeTime());
    }

    IEnumerator FadeTime ()
    {
        yield return new WaitForSeconds(PlayerHealth.Instance.invincibleTimer);
        Color tmp = spriteRenderer.color;
        tmp.a = 1f;
        spriteRenderer.color = tmp;
    }
}
