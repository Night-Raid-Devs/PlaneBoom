using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBubbleScript : MonoBehaviour {

    public long addScore = 50;

    public int addTime = 5;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AircraftJet")
        {
            other.gameObject.GetComponent<PlayerHpScript>().AddScore(addScore);
            other.gameObject.GetComponent<PlayerHpScript>().AddTime(addTime);
            other.gameObject.GetComponent<RocketLauncher>().RechargeRockets();
            Destroy(gameObject);
        }
    }
}
