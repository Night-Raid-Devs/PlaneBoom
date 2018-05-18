using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher2 : MonoBehaviour {

    public GameObject missile;
    public GameObject Barrel;
    public float Delay = 10.0f;
    public float FireRange = 2000f;

    private GameObject Target;
    float timeToFire = 0.1f;

    void Start()
    {
        Target = GameObject.Find("AircraftJet");
    }

    void Update () {
        timeToFire += Time.deltaTime;

        Vector3 targetTransform = Target.transform.position - Barrel.transform.position;

        if (timeToFire > Delay)
        {
            if (Mathf.Abs(targetTransform.x) < FireRange && Mathf.Abs(targetTransform.y) < FireRange && Mathf.Abs(targetTransform.z) < FireRange)
            {
                Instantiate(missile, transform.position, transform.rotation);
                timeToFire = 0f;
            }
        }
    }
}
