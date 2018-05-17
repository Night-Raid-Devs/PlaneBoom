using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrackRadius : MonoBehaviour {

    public float radius = 2000;
	
	void Start () {
        List<Transform> children = new List<Transform>(4);
        for (int i = 1; i < 4; i++)
        {
            children.Add(transform.GetChild(i));
        }

        children[0].localPosition = new Vector3(0, 0, radius);
        children[1].localPosition = new Vector3(-radius, 0, radius);
        children[2].localPosition = new Vector3(-radius, 0, 0);
    }
}
