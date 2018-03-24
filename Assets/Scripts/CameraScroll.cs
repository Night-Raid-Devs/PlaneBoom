using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

	void Update () {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - 1);
        }
    }
}
