using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Loading : MonoBehaviour {

    // Use this for initialization

    public Image loadingImg;
    public float step;
    bool active = false;

	void Start () {
       step = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            active = true;
        }
        if(active)
        { 
            if (step >= 0)
            {
                loadingImg.fillAmount = step;
                step -= 0.005f;
            }
            else {active = false; step = 1; }
        }
	}
}
