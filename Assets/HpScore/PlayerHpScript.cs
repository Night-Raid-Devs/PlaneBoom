using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpScript : MonoBehaviour
{
    public Image healthBar;

    public float fullHp = 100;
    private const float deathTime = 0.705f;

    private float currentHp;

    private bool isAlive = true;

    private AudioSource audioSource;
    private Collider objectCollider;

    void Start()
    {
        currentHp = fullHp;
        audioSource = GetComponent<AudioSource>();
        objectCollider = GetComponent<Collider>();
    }

    public void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            currentHp = 0;
            healthBar.fillAmount = currentHp / fullHp;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            objectCollider.enabled = false;
            GetComponent<ExplodeForPlane>().SpawnExplosion();
            //Destroy(gameObject, deathTime);
        }
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "turretBullet")
        {
            this.SetDamage(2);
            Destroy(collision.gameObject);
        }
    }
}
