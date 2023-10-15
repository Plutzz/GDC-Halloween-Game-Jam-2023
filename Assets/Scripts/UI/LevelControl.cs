using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelControl : MonoBehaviour
{
    public static LevelControl Instance;
    public GameObject deathScreen;
    public TextMeshProUGUI rounds;
    public EnemySpawner enemySpawner;
   
    private void Awake()
    {
        Instance= this;
    }

    public void ReturnToTitle ()
    {
        //PauseMenu.Instance.Resume();
        SceneManager.LoadScene("Main Menu");
    }

    public void ReloadScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver ()
    {
        deathScreen.SetActive(true);
        rounds.text = enemySpawner.GetRounds().ToString();
    }
}
