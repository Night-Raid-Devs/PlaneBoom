using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHpScript : MonoBehaviour
{
    public Canvas objectCanvas;

    public Image healthBar;

    public float fullHp = 100;

    private const float modelRemoveTime = 0.2f;
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

    void Update()
    {
        if (objectCanvas != null)
        {
            objectCanvas.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, transform.up);
        }
    }

    private void Die()
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
            Destroy(gameObject, deathTime);
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
        if (collision.gameObject.name == "playerBullet")
        {
            this.SetDamage(2);
            Destroy(collision.gameObject);
        }
    }
}
