using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BordersScript : MonoBehaviour {
    
    private float counter = -1;
    public Image loadingImg;
    public Text counterText;
    public Text warningText;

    private float updateTime = 0;
    private Collider obj;
    private float step = 1;


    private void Start()
    {     
        loadingImg.enabled = false;
        warningText.enabled = false;
    }

    void Update () {
        if (counter != -1)
        {
            if (counter < 0)
            {
                loadingImg.fillAmount = 0;
                step = 1;

                warningText.enabled = false;    
                counter = -1;
                obj.GetComponent<PlayerHpScript>().Die();
            }
            else
            {
                warningText.enabled = true;
                loadingImg.enabled = true;
                counter = counter - 0.01f;    

                if (counter >= 0)
                {
                    counterText.text = counter.ToString("f1");                
                }
                if (step > 0)
                {
                    step -= 0.0020f;
                    loadingImg.fillAmount = step;
                }

                updateTime = 0;
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
            step = 1;
            counterText.text = string.Empty;
            loadingImg.enabled = false;
            warningText.enabled = false;
        }
    }
}
