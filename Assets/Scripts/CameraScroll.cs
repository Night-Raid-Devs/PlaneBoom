using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

    public Camera myCamera;
    private int zoom;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            zoom = 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            zoom = -1;
        }
        else
        {
            zoom = 0;
        }

        myCamera.transform.localPosition = new Vector3(myCamera.transform.localPosition.x, myCamera.transform.localPosition.y, myCamera.transform.localPosition.z + zoom);
    }
}
