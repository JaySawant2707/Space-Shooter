using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] float healAmount = 30f;
    [SerializeField] bool isHeal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isHeal)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Health>().UpdateHealth(healAmount);
                Destroy(gameObject);
            }
        }

    }
}
