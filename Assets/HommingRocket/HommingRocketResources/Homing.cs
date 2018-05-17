using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {

    public string targetTag = "target";
    public float missileVelocity = 100;
    public float turn = 20;
    public Rigidbody homingMissile;
    public float fuseDelay;
    public GameObject missileMod;
    public ParticleSystem SmokePrefab;
    public AudioClip missileClip;

    private Transform target;

    private bool isNotStarted = true;

	// Use this for initialization
	void Start () {
        var rocketCollider = this.missileMod.GetComponent<Collider>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Physics.IgnoreCollision(rocketCollider, obj.GetComponent<Collider>());
        }

        ////var playerColliders = GameObject.FindGameObjectsWithTag("playerCollider");
        ////foreach (var collider in playerColliders)
        ////{
        ////    Physics.IgnoreCollision(rocketCollider, collider.GetComponent<Collider>());
        ////}

        StartCoroutine(Fire());
    }

    void FixedUpdate()
    {
        if (isNotStarted)
        {
            homingMissile.AddForce(Vector3.down * 100, ForceMode.Impulse);
        }

        if (target == null)
            return;

        homingMissile.velocity = transform.forward * missileVelocity;
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }

    private IEnumerator Fire()
    {
        var oldEmRate = SmokePrefab.emissionRate;
        SmokePrefab.emissionRate = 0.0f;

        yield return new WaitForSeconds(fuseDelay);
        isNotStarted = false;
        //rocketCollider.enabled = true;

        SmokePrefab.emissionRate = oldEmRate;

        AudioSource.PlayClipAtPoint(missileClip, transform.position);

        var distance = Mathf.Infinity;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag(targetTag))
        {
            var diff = (go.transform.position - transform.position).sqrMagnitude;
            if (diff < distance)
            {
                distance = diff;
                target = go.transform;
            }
        }
    }

    private void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == targetTag)
        {
            //theCollision.gameObject.GetComponent<PlayerHpScript>().SetDamage(20);
            SmokePrefab.emissionRate = 0.0f;
            //Destroy(missileMod.gameObject);
            //yield WaitForSeconds(5);
            //Destroy(gameObject);
        }
    }
}
