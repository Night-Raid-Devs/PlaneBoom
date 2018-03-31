using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHpScript : MonoBehaviour
{
    public Canvas objectCanvas;

    public Image healthBar;

    public float fullHp = 100;

    public float canvasHeight;

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
        objectCanvas.transform.localPosition = this.transform.InverseTransformDirection(Camera.main.transform.up * canvasHeight);
        objectCanvas.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
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
}
