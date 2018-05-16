using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {

    private TimeSpan levelTime;

    public Text timeText;
    public int levelTimeSeconds = 180;

    private void Start()
    {
        levelTime = TimeSpan.FromSeconds(levelTimeSeconds);
        StartCoroutine(LevelTiming());
    }

    void Update () {
		if (Input.GetKeyDown(KeyCode.F2))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex < 5)
            {
                SceneManager.LoadScene(sceneIndex + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex > 1)
            {
                SceneManager.LoadScene(sceneIndex - 1);
            }
        }
	}

    public void AddLevelTime(int seconds)
    {
        levelTime += TimeSpan.FromSeconds(seconds);
        ShowTime();
    }

    private IEnumerator LevelTiming()
    {
        while (levelTime.TotalSeconds > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            levelTime -= new TimeSpan(0, 0, 1);
            ShowTime();
        }

        GameObject.Find("AircraftJet").GetComponent<PlayerHpScript>().Die();
    }

    private void ShowTime()
    {
        DateTime dateTime = new DateTime();
        dateTime += levelTime;
        timeText.text = dateTime.ToString("mm:ss");
    }
}
