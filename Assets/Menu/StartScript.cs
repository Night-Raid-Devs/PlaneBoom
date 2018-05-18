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
    }
}
