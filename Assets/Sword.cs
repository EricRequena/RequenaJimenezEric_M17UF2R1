using UnityEngine;

public class Sword : MonoBehaviour
{
    public SwordSO swordSO;

    void Start()
    {
        if (swordSO == null)
        {
            Debug.LogError("SwordSO no está asignado. Por favor, configúralo en el inspector.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (swordSO.isAttacking && other.CompareTag("Enemy"))
        {
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(swordSO.damage);
               
            }
        }
    }
}
