using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpScript : MonoBehaviour
{
    public Text counterText;
    public Image healthBar;

    public Text scoreText;

    public float fullHp = 100;
    private const float deathTime = 0.705f;

    private float currentHp;

    private long currentScore = 0;

    private bool isAlive = true;

    private AudioSource audioSource;
    private Collider objectCollider;
    private GameControlScript gameControlScript;

    void Start()
    {
        currentHp = fullHp;
        audioSource = GetComponent<AudioSource>();
        objectCollider = GetComponent<Collider>();
        gameControlScript = GameObject.Find("Terrain").GetComponent<GameControlScript>();
    }

    public void Die(bool isVictory = false)
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
            if (!isVictory)
            {
                counterText.text = "GAME OVER";
                GetComponent<ExplodeForPlane>().SpawnExplosion(true);
            }
            else
            {
                counterText.text = "VICTORY!!!";
                GetComponent<ExplodeForPlane>().SpawnExplosion(false);
            }
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

    public void AddHealth(float hp)
    {
        if (currentHp + hp > fullHp)
        {
            currentHp = fullHp;
        }
        else
        {
            currentHp += hp;
        }

        healthBar.fillAmount = currentHp / fullHp;
    }

    public void AddScore(long addedScore)
    {
        currentScore += addedScore;
        scoreText.text = currentScore.ToString();
    }

    public void AddTime(int seconds)
    {
        gameControlScript.AddLevelTime(seconds);
    }

    public void OnDetonatorForceHit(bool isPlayerRocket)
    {
        if (!isPlayerRocket)
        {
            SetDamage(30);
        }
    }
}
