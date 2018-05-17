using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyScript : MonoBehaviour {

    public GameObject carTurret;

	void Update () {
		if (carTurret == null)
        {
            Destroy(gameObject);
        }
	}
}
