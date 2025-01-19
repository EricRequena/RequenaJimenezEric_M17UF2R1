using UnityEngine;

public abstract class EnemyControlller : MonoBehaviour
{
    public float velocidad = 5f;

    public virtual void Mover(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    public abstract void RealizarAccion(); 
}
