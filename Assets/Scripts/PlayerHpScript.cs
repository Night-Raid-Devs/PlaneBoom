using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpScript : MonoBehaviour
{
    public Image healthBar;
    public Image healthBG;

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
        if (!isAlive)
        {
            if (audioSource.time >= modelRemoveTime)
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    private void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            audioSource.Play();
            healthBG.enabled = false;
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
}
