using System.Collections;
using UnityEngine;

public class ExplosiveEnemy : Enemy, ITakeDamage
 
{
    private Rigidbody2D rb;              
    private GameObject player;      
    
    public float stopDistance = 2f;
    public float Aimation = 10f;

    public float speed = 1f;           
    public bool explodeOnDeath = true;
  
    private float distanceToTarget = 0f;
    public Animator animExplote;

    void Start()
    {
       base.Start();
        rb = GetComponent<Rigidbody2D>(); 
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
       
        if (!GetComponent<Renderer>().isVisible)
        {
            return; 
        }

        distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        if (player != null)
        {
            FollowPlayer(player.transform, speed, stopDistance);
        }

       

        if (distanceToTarget < stopDistance)
        {
            player.GetComponent<ITakeDamage>().TakeDamage(20);
            Die();
        }

        if (distanceToTarget < Aimation)
        {
            animExplote.SetBool("IsExplote", true);
            StartCoroutine(ExploteDelay(0.5f));
        }
    }





    public void TakeDamage(float damage)
    {
    
        hp -= damage;
        slider.value = hp;
        if (hp <= 0)
        {
            explodeOnDeath = false;
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

    IEnumerator ExploteDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Espera el tiempo especificado
        animExplote.SetBool("IsExplote", false);
    }


    public void Die()
    {
      Destroy(gameObject);

        if ( explodeOnDeath == false)
        {
            NewBehaviourScript.points += 10;
            Debug.Log("Puntos: " + NewBehaviourScript.points);
        }
        
    }
}
