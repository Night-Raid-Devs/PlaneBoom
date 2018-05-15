using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAirBomb : MonoBehaviour {
    private Camera planeCam;
    private Camera bombCam;
    private bool notDropped = true;

    private void ChangeCameras()
    {
        if (bombCam == null)
        {
            var plCam = GameObject.Find("Camera").GetComponent<Camera>();
            plCam.enabled = true;
            plCam.tag = "MainCamera";
            Destroy(this);
            return;
        }

        if (planeCam.enabled)
        {
            planeCam.enabled = false;
            bombCam.enabled = true;
            planeCam.tag = "bombCam";
            bombCam.tag = "MainCamera";
        }
        else
        {
            bombCam.enabled = false;
            planeCam.enabled = true;
            bombCam.tag = "bombCam";
            planeCam.tag = "MainCamera";
        }
    }

    void Start()
    {
        planeCam = Camera.main;
    }

    void Update()
    {
        if (notDropped && Input.GetKeyDown(KeyCode.G))
        {
            notDropped = false;
            var airBomb = GameObject.Find("AirBombPref");
            Destroy(airBomb);
            Rigidbody airBombRigidBody = airBomb.AddComponent<Rigidbody>();
            airBombRigidBody.mass = 3000;
            airBomb.AddComponent<AirBombRotation>();
            Instantiate(airBomb, this.transform, true);
            bombCam = FindObjectOfType<Camera>();
        }

        if (!notDropped && Input.GetKeyDown(KeyCode.R))
        {
            ChangeCameras();
        }
    }
}
