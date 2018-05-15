using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

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
}
