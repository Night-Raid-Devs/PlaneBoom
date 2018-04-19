using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour {

    public string CollisionTag = "terrain";
    public GameObject currentDetonator;
    public float explosionLife = 10;
    public float detailLevel = 10.0f;

    private void Start()
    {
    }

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
            DestroyObject(this.gameObject);
        }
    }
}
