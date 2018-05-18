using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScoresScript : MonoBehaviour {

	void Start () {
        GetComponent<Scores>().GetScores();
    }
}
