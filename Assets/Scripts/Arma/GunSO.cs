using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GunSO", menuName = "Scriptable Objects/Weapons/GunSO")]

public class GunSO : AWeaponSO
{
    public override void Attack()
    {
        AudioManager.instance.PlayGunAudio();

        GameObject bullet = gameWeapon.GetComponentInChildren<BulletPool>().GetBullet();

        bullet.transform.position = gameWeapon.GetComponentInChildren<Transform>().position;

        bullet.GetComponent<Ball>().SetDirection(gameWeapon.GetComponentInChildren<Transform>().position);
    }

    public override void StopAttack()
    {

    }
}
