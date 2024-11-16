using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] float health = 50f;
    [SerializeField] int scorePoints = 10;
    [SerializeField] bool applyCameraShake = false;
    [SerializeField] ParticleSystem hitEffect;

    CameraShake cameraShake;
    AudioManager audioManager;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Loot loot;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
        loot = FindFirstObjectByType<Loot>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            ShakeCamera();
            audioManager.PlayDamageVolume();
            PlayHitEffect();
            damageDealer.Hit();//Destroy damage dealer (bullet)
        }
    }

    void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.UpdateScore(scorePoints);
            GivePowerUp();
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void GivePowerUp()
    {
        if (Random.Range(1, 4) == 1)
        {
            GameObject powerUp = loot.GetRandomLoot();
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }

    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void UpdateHealth(float amount)
    {
        health += amount;
    }
}
