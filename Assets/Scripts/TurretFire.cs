using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{

    public GameObject Bullet;
    public float BulletForwardForce;
    public GameObject BulletEmitter;
    public GameObject BulletEmitter2;

    private int bulletDelay = 0;
    private bool isFirstGun = true;

    private void Shoot(GameObject bulletEmitter)
    {
        GameObject bulletHandler = Instantiate(Bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
        bulletHandler.name = "turretBullet";
        var playerColliders = GameObject.FindGameObjectsWithTag("playerCollider");
        foreach (GameObject collider in playerColliders)
        {
            Physics.IgnoreCollision(bulletHandler.GetComponent<Collider>(), collider.GetComponent<Collider>());
        }

        Rigidbody rigidBody = bulletHandler.GetComponent<Rigidbody>();
        rigidBody.AddForce(-bulletEmitter.transform.right * BulletForwardForce);
        Destroy(bulletHandler, 3.0f);
    }

    void Update()
    {
        if (bulletDelay <= 0)
        {
            bulletDelay = Random.Range(0, 10);
            if (isFirstGun)
            {
                Shoot(BulletEmitter);
            }
            else
            {
                Shoot(BulletEmitter2);
            }
            isFirstGun = !isFirstGun;
        }
        bulletDelay--;
    }
}
