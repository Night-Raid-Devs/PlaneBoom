using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHeartScript : MonoBehaviour {

    public long addScore = 30;

    public int addHealth = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AircraftJet")
        {
            other.gameObject.GetComponent<PlayerHpScript>().AddScore(addScore);
            other.gameObject.GetComponent<PlayerHpScript>().AddHealth(addHealth);
            Destroy(gameObject);
        }
    }
}
