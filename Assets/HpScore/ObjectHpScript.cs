using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHpScript : MonoBehaviour
{
    public Canvas objectCanvas;

    public Image healthBar;

    public float fullHp = 100;

    private const float deathTime = 0.1f;

    private float currentHp;

    private bool isAlive = true;

    private AudioSource audioSource;
    private Collider objectCollider;


    public float explosionLife = 10;
    public GameObject explosionDetonator;
    public float explosionDetailLevel = 10.0f;
    public float explosionSize = 50;

    void Start()
    {
        currentHp = fullHp;
        audioSource = GetComponent<AudioSource>();
        objectCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (objectCanvas != null)
        {
            objectCanvas.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, transform.up);
        }
    }

    public void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            if (audioSource != null)
            {
                audioSource.Play();
            }

            objectCanvas.enabled = false;
            objectCollider.enabled = false;
            SpawnExplosion();
            Destroy(gameObject, deathTime);
        }
    }

    private void SpawnExplosion()
    {
        GameObject exp = Instantiate(explosionDetonator, transform.position, Quaternion.identity);
        Detonator dTemp = (Detonator)exp.GetComponent("Detonator");
        dTemp.detail = explosionDetailLevel;
        dTemp.size = explosionSize;
        Destroy(exp, explosionLife);
    }

    public void SetDamage(float damage)
    {
        if (isAlive)
        {
            currentHp -= damage;
            healthBar.fillAmount = currentHp / fullHp;
            if (currentHp <= 0)
            {
                this.Die();
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "playerBullet")
        {
            this.SetDamage(2);
            Destroy(collision.gameObject);
        }
    }
}
