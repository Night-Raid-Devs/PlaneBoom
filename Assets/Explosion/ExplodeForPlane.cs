using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodeForPlane : MonoBehaviour {

    public string CollisionTag = "terrain";
    public GameObject currentDetonator;
    public float explosionLife = 10;
    public float detailLevel = 10.0f;

    public void SpawnExplosion()
    {
        GameObject exp = (GameObject)Instantiate(currentDetonator, transform.position, Quaternion.identity);
        Detonator dTemp = (Detonator)exp.GetComponent("Detonator");
        dTemp.detail = detailLevel;
        //dTemp.Explode();
        Destroy(exp, explosionLife);
        var cam = GameObject.Find("Camera").GetComponent<Camera>();
        cam.enabled = true;
        var cameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].name != "Camera") cameras[i].enabled = false;
        }

        var newCam = Instantiate(cam, Camera.main.transform.position, Camera.main.transform.rotation);
        DestroyObject(this.gameObject);
        newCam.GetComponent<MonoBehaviour>().StartCoroutine(BackToMenu());
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == CollisionTag)
        {
            GetComponent<PlayerHpScript>().Die();
        }
    }
}
