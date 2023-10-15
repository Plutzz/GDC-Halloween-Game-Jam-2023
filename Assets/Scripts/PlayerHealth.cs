using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : DamageableEntity
{
    public int maxHp = 100;
    public Image health;
    public GameObject gameOverUI;

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
        if(currentHp > 0)
        {
            currentHp -= damage;
        }
        
        float fillvalue = Mathf.Clamp((float)currentHp/(float)maxHp, 0, maxHp);

        // Debug.Log(fillvalue);

        health.fillAmount = fillvalue;

        if(currentHp <= 0)
        {
            gameOverUI.SetActive(true);
        }
    }

}