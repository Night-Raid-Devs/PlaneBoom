using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour {

    public string CollisionTag = "terrain";
    public GameObject currentDetonator;
    public float explosionLife = 10;
    public float detailLevel = 10.0f;
    public float explodeSize = 10;
    public bool isPlayerRocket;

    private void Start()
    {
        currentDetonator.GetComponent<DetonatorForce>().isPlayerRocket = isPlayerRocket;
    }

    private void SpawnExplosion()
    {
        GameObject exp = Instantiate(currentDetonator, transform.position, Quaternion.identity);
        Detonator dTemp = (Detonator)exp.GetComponent("Detonator");
        dTemp.detail = detailLevel;
        dTemp.size = explodeSize;
        //dTemp.Explode();
        Destroy(exp, explosionLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == CollisionTag || collision.gameObject.tag == "terrain")
        { 
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
