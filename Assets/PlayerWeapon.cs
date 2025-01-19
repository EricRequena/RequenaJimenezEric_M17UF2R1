using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public AWeaponSO CurrentWeapon;
    public Transform gun;
    public SpriteRenderer gunSR;
    public int speedball;
    public int damage;
    Vector3 targetRotation;

    public GameObject ball;

    public AWeaponSO [] gunsSO;

    private void Start()
    {
        CurrentWeapon = gunsSO[0];
        gunsSO[0].gameWeapon = Instantiate(gunsSO[0].prefabWeapon, transform.position, transform.rotation, transform);
    }


    private void Update ()
    {
        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);
        float angle = Mathf.Atan2 (targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));

        if (angle > 90 || angle < -90)
        {
            gunSR.flipY = true;
        }
        else
        {
            gunSR.flipY = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CurrentWeapon.Attack();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CurrentWeapon.StopAttack();
        }

        // Cambiar el sprite según la tecla presionada
        if (Input.GetKeyDown (KeyCode.Alpha1)) // Tecla 1
        {
            CurrentWeapon.gameWeapon.SetActive(false);
            if (gunsSO[0].gameWeapon == null)
            {
                gunsSO[0].gameWeapon = Instantiate(gunsSO[0].prefabWeapon, transform.position, transform.rotation, transform);
            } 
            else
            {
                gunsSO[0].gameWeapon.SetActive(true);
            }
            CurrentWeapon = gunsSO[0];
        }
        else if (Input.GetKeyDown (KeyCode.Alpha2)) // Tecla 2
        {
            CurrentWeapon.gameWeapon.SetActive(false);
            if (gunsSO[1].gameWeapon == null)
            {
                gunsSO[1].gameWeapon = Instantiate(gunsSO[1].prefabWeapon, transform.position, transform.rotation, transform);
            } 
            else
            {
                gunsSO[1].gameWeapon.SetActive(true);
            }
            CurrentWeapon = gunsSO[1];

        }
        else if (Input.GetKeyDown (KeyCode.Alpha3)) // Tecla 3
        {
            CurrentWeapon.gameWeapon.SetActive(false);
            if (gunsSO[2].gameWeapon == null)
            {
                gunsSO[2].gameWeapon = Instantiate(gunsSO[2].prefabWeapon, transform.position, transform.rotation, transform);
            } 
            else
            {
                gunsSO[2].gameWeapon.SetActive(true);
            }
            CurrentWeapon = gunsSO[2];

        }
    }
}
