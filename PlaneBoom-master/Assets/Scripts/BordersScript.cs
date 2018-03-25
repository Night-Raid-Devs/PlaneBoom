using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BordersScript : MonoBehaviour {
    
    private int counter = -1;
    public Text counterText;
    private float updateTime = 0;
    Collider obj;
	
	void Update () {
        if (counter != -1)
        {
            if (updateTime >= 1)
            {               
                counterText.text = counter.ToString();
                counter--;
                updateTime = 0;

                if (counter == 0)
                {
                    Destroy(obj.gameObject);
                }
            }
            updateTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AircraftJet" && counter == -1)
        {
            obj = other;
            updateTime = 0;
            counter = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AircraftJet" && counter != -1)
        {
            counter = -1;
        }
    }
}
