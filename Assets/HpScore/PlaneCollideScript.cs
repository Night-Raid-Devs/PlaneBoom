using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollideScript : MonoBehaviour {

    public PlayerHpScript objectToHit;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "playerBullet")
        {
            if (objectToHit != null)
            {
                objectToHit.SetDamage(2);
            }

            Destroy(collision.gameObject);
        }
    }
}
