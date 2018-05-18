using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneScript : MonoBehaviour {

    public InputField inputNick;

    public void LoadByIndex(int sceneIndex)
    {
        if (sceneIndex >= 1 && sceneIndex <= 3)
        {
            PlayerPrefs.SetString("nick", inputNick.text);
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
