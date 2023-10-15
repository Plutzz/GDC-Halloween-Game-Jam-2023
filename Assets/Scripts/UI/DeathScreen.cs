using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject timeLived;
    [SerializeField] private GameObject numKills;
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject deathMusic;



    private void Start()
    {
        timerText.SetActive(false);
        timeLived.SetActive(true);
        numKills.SetActive(true);
        timeLived.GetComponent<TextMeshProUGUI>().text = Timer.timer.GetTime();
        numKills.GetComponent<TextMeshProUGUI>().text = Enemy.numKills + " Kills";




       


    }


    private void OnEnable()
    {
        Destroy(music);

        Instantiate(deathMusic);
    }

}
