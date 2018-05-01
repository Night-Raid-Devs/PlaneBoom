using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBombRotation : MonoBehaviour {
    public float turn = 0.4f;
    public float speedDown = 5000; 
    private Transform target;
    private Rigidbody rocketBody;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("terrain").transform;
        rocketBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        rocketBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
        rocketBody.AddForce(targetRotation * Vector3.down * speedDown, ForceMode.Impulse);
    }
}
