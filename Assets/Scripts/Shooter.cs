using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool usedByAI = false;
    [SerializeField] float minFiringRate = 0.1f;
    [SerializeField] float firingRateVariance = 0f;

    [HideInInspector]public bool isFiring;
    Coroutine firingCorotine;
    AudioManager audioManager;

    void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void Start()
    {
        if (usedByAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCorotine == null)
        {
            firingCorotine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCorotine != null)
        {
            StopCoroutine(firingCorotine);
            firingCorotine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            audioManager.PlayShootingVolume();

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeBetweenFire = Random.Range(baseFiringRate + firingRateVariance, baseFiringRate - firingRateVariance);
            Mathf.Clamp(timeBetweenFire, minFiringRate, float.MaxValue);
            
            yield return new WaitForSeconds(timeBetweenFire);

        }

    }
}
