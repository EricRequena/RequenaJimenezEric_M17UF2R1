using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FireSO", menuName = "Scriptable Objects/Weapons/FireSO")]

public class FireSO : AWeaponSO
{
    public override void Attack()
    {
        AudioManager.instance.PlayFireballAudio();

        gameWeapon.GetComponentInChildren<ParticleSystem>().Play();
    }

    public override void StopAttack()
    {
        AudioManager.instance.StopFireballAudio();
        gameWeapon.GetComponentInChildren<ParticleSystem>().Stop();
    }

}
