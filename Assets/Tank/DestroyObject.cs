using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public float DestroyTime;
    public GameObject destroyObj;
    float time = 0f;

    void Update () {
        time += Time.deltaTime;
        if(time >= DestroyTime)
        {
            Destroy(destroyObj);
            time = 0;
        }
	}
}
