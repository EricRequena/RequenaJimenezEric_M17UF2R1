using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private Stack<GameObject> bullets;

    private void Awake()
    {
        bullets = new Stack<GameObject>();
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            GameObject bullet = bullets.Pop();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            return Instantiate(bulletPrefab);
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Push(bullet);
    }
}
