using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamageScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.GetComponent<ObjectHpScript>().SetDamage(100);
        }
    }
}
