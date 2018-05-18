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

    public GameObject bubbleObject;
    public GameObject heartObject;

    private void Start()
    {
        levelTime = TimeSpan.FromSeconds(levelTimeSeconds);
        StartCoroutine(LevelTiming());

        for (int i = 0; i < 100; i++)
        {
            float x = UnityEngine.Random.Range(1500, 8500);
            float y = UnityEngine.Random.Range(0, 3500);
            float z = UnityEngine.Random.Range(1500, 8500);

            Instantiate(bubbleObject, new Vector3(x, y, z), UnityEngine.Random.rotation);
        }

        for (int i = 0; i < 100; i++)
        {
            float x = UnityEngine.Random.Range(1500, 8500);
            float y = UnityEngine.Random.Range(0, 3500);
            float z = UnityEngine.Random.Range(1500, 8500);

            Instantiate(heartObject, new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0)));
        }
    }

    void Update () {
		if (Input.GetKeyDown(KeyCode.F2))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex < 3)
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

            if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
            {
                GameObject.Find("AircraftJet").GetComponent<PlayerHpScript>().Die(true);
                yield return new WaitForSecondsRealtime(5);
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                if (sceneIndex < 3)
                {
                    SceneManager.LoadScene(sceneIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }

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
