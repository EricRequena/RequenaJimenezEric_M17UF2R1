using UnityEngine;
using System.Collections;

public class Lanzador : Enemy, ITakeDamage
{
    
    public GameObject bolaPrefab;
    public float velocidad = 5f;

    
    public float intervaloDisparo = 2f;
    private Transform jugador;
    public Animator shootAnim;
    private void Start()
    {
        base.Start();
        shootAnim = GetComponent<Animator>();

        jugador = GameObject.FindWithTag("Player").transform;

        InvokeRepeating("RealizarAccion", 0f, intervaloDisparo);
    }

    private void Update()
    {
        
    }

    public void RealizarAccion()
    {
        shootAnim.SetBool("IsShooting", true);
        StartCoroutine(isShootDelay(0.1f));
        if (!GetComponent<Renderer>().isVisible)
        {
            return;
        }

        if (bolaPrefab != null)
        {
            GameObject bola = Instantiate(bolaPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bola.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector3 direccion = (jugador.position - transform.position).normalized;
                rb.AddForce(direccion * velocidad, ForceMode2D.Impulse);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        slider.value = hp;
        if (hp <= 0)
        {
            Die();
        }

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ExecuteAfterDelay(0.2f));
    }

    IEnumerator ExecuteAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Espera el tiempo especificado
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    IEnumerator isShootDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Espera el tiempo especificado
        shootAnim.SetBool("IsShooting", false);
    }


    public void Die()
    {
        NewBehaviourScript.points += 10;
        Debug.Log("Puntos: " + NewBehaviourScript.points);
        Destroy(gameObject);
    }
}
