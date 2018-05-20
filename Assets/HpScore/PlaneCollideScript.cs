using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollideScript : MonoBehaviour {

    public PlayerHpScript objectToHit;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "turretBullet")
        {
            if (objectToHit != null)
            {
                objectToHit.SetDamage(2);
            }

            Destroy(collision.gameObject);
        }
    }
}
