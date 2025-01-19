using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, ITakeDamage
{

    private Animator animator;
    private new Rigidbody2D rigidbody;
    public SwordSO sword;
    public FireSO fire;
    public GunSO gun;

    public float health = 300;
    public float speed;

    public static int points = 0;

    private Vector3 velocity;

    void Start ()
    {
        animator = GetComponent<Animator> ();
        rigidbody = GetComponent<Rigidbody2D> ();
    }


    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (hor != 0 || ver != 0)
        {
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Speed", 1);

            Vector3 direction = (Vector3.up * ver + Vector3.right * hor).normalized;
            velocity = direction * speed;
        }
        else
        {
            animator.SetFloat("Speed", 0);
            velocity = Vector3.zero;
        }

        if (health <= 0)
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Tecla P presionada"); 
            Debug.Log("Puntos: " + points);
            pressP();
        }
    }


    void FixedUpdate ()
    {
        rigidbody.MovePosition (transform.position + velocity * Time.fixedDeltaTime );
    }

    public void isDeath ()
    {

    }

    public void TakeDamage(float damage)
    {
        if (Tutorial.numTXT >= 9)
        {
            health -= damage;
        }
        
    }

    public void Die()
    {
        sword.damage = 15;
        fire.damage = 15;
        gun.damage = 15;
        Destroy(gameObject);
        Application.Quit();
    }

    public void pressP()
    {
        if (points >= 1)
        {
            points -= 2;
            sword.damage += 5;
            fire.damage += 5;
            gun.damage += 5;

        }
    }


}
