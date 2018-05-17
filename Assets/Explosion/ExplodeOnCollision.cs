﻿using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour {

    public string CollisionTag = "terrain";
    public GameObject currentDetonator;
    public float explosionLife = 10;
    public float detailLevel = 10.0f;
    public float explodeSize = 10;
    public float radius = 100;

    public bool isPlayerRocket;

    private void SpawnExplosion()
    {
        GameObject exp = Instantiate(currentDetonator, transform.position, Quaternion.identity);
        exp.GetComponent<DetonatorForce>().SetIsPlayerRocket(isPlayerRocket);
        Detonator dTemp = (Detonator)exp.GetComponent("Detonator");
        dTemp.detail = detailLevel;
        dTemp.size = explodeSize;
        exp.GetComponent<DetonatorForce>().radius = radius;
        //dTemp.Explode();
        Destroy(exp, explosionLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == CollisionTag || collision.gameObject.tag == "terrain")
        { 
            if (gameObject.name.StartsWith("HommingMissile") && collision.gameObject.tag == CollisionTag)
            {
                collision.gameObject.GetComponent<PlayerHpScript>().SetDamage(30);
            }

            SpawnExplosion();
            if (name.StartsWith("AirBombPref"))
            {
                var mainCam = Camera.main;
                if (mainCam.name != "Camera")
                {
                    mainCam.name = "BombCam";
                    Instantiate(mainCam, mainCam.transform.position, mainCam.transform.rotation);
                }
            }

            DestroyObject(this.gameObject);
        }
    }
}
