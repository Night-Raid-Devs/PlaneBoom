using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeForPlane : MonoBehaviour {

    public string CollisionTag = "terrain";
    public GameObject currentDetonator;
    public float explosionLife = 10;
    public float detailLevel = 10.0f;

    private void SpawnExplosion()
    {
        GameObject exp = (GameObject)Instantiate(currentDetonator, transform.position, Quaternion.identity);
        Detonator dTemp = (Detonator)exp.GetComponent("Detonator");
        dTemp.detail = detailLevel;
        //dTemp.Explode();
        Destroy(exp, explosionLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == CollisionTag)
        {
            SpawnExplosion();

            var cam = GameObject.Find("Camera").GetComponent<Camera>();
            cam.enabled = true;
            var cameras = FindObjectsOfType<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i].name != "Camera") cameras[i].enabled = false;
            }

            Instantiate(cam, Camera.main.transform.position, Camera.main.transform.rotation);
            DestroyObject(this.gameObject);
        }
    }
}
