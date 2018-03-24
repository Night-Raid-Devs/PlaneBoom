using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BordersScript : MonoBehaviour {
    
    private int counter = -1;
    public Text counterText;
    private float updateTime = 0;

	void Start () {
		
	}
	
	void Update () {
        if (counter != -1)
        {
            if (updateTime >= 1)
            {
                counterText.text = counter.ToString();
                counter = counter--;
                updateTime = 0;
            }
            updateTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AircraftJet" && counter == -1)
        {
            updateTime = 0;
            counter = 5;
        }
    }
}
