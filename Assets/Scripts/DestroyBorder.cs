﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBorder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
