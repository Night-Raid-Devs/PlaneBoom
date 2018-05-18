using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

    public InputField inputNick;

    public void Start()
    {
        if (inputNick != null)
        {
            inputNick.text = "Player" + Random.Range(1, 100);
        }

        if (PlayerPrefs.GetInt("rlevel", -1) != -1)
        {
            GetComponent<Scores>().AddNewScore(PlayerPrefs.GetString("rnick"), PlayerPrefs.GetInt("rvalue"), PlayerPrefs.GetInt("rlevel"));
            PlayerPrefs.SetInt("rlevel", -1);
        }
    }
}
