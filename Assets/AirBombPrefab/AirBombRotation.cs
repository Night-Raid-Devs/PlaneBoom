using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBombRotation : MonoBehaviour {
    public float turn = 0.4f;
    public float speedDown = 5000; 
    private Transform target;
    private Rigidbody rocketBody;
    private Vector3 speedVector;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("terrain").transform;
        rocketBody = GetComponent<Rigidbody>();
        speedVector = Vector3.down * speedDown;
    }

    void FixedUpdate()
    {
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        rocketBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
        rocketBody.AddForce(targetRotation * speedVector, ForceMode.Impulse);
    }
}
