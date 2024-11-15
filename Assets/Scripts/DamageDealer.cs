using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 5f;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
