using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireThrower : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collided with " + other.name);
        if (other.CompareTag("Enemy"))
        {
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(20);

            }
        }
    }
}
