using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float _speed = 15f;
    private float timeToDestroy = 2f;
    private float timeToDestroyCounter;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timeToDestroyCounter = 0;
    }

    public void SetDirection(Vector2 initialDirection)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePosition - initialDirection).normalized;

        _rb.velocity = direction * _speed;
    }

    private void Update()
    {
        timeToDestroyCounter += Time.deltaTime;
        if (timeToDestroyCounter >= timeToDestroy)
        {
            gameObject.SetActive(false);
        }
    }  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            ITakeDamage damageable = other.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(20);
            }
        }
        if (other.CompareTag("Pared") || other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        FindAnyObjectByType<BulletPool>().ReturnBullet(gameObject);
    }
}
