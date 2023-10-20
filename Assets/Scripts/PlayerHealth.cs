using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : DamageableEntity
{
    public int maxHp = 100;
    public Image health;
    public GameObject gameOverUI;
    private bool invincible = false;
    public float invincibleTimer = 1f;

    public static PlayerHealth Instance;

    void Awake ()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    public override void takeDamage (int damage)
    {
        if(currentHp > 0 && !invincible)
        {
            currentHp -= damage;

            SoundManager.Instance.PlaySound(SoundManager.Sounds.Hit);

            InvincibleIndicator.Instance.FadeCharacter();

            invincible = true;

            Debug.Log("Invincible");

            float fillvalue = Mathf.Clamp((float)currentHp/(float)maxHp, 0, maxHp);

            // Debug.Log(fillvalue);

            health.fillAmount = fillvalue;

            if(currentHp <= 0)
            {
                OnDeath();
            }

            StartCoroutine(Invincibililty());
        }
    }

    private IEnumerator Invincibililty()
    {
        yield return new WaitForSeconds(invincibleTimer);
        invincible = false;
        Debug.Log("no longer invincible");
    }

    protected override void OnDeath()
    {
        LevelControl.Instance.GameOver();
    }

}