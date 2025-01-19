using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponSO : ScriptableObject
{
    public int damage;
    public GameObject prefabWeapon;
    public GameObject gameWeapon;
    public int costWeapon;
    public abstract void Attack();
    public abstract void StopAttack();
}
