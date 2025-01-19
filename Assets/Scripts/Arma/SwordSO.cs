using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordSO", menuName = "Scriptable Objects/Weapons/SwordSO")]
public class SwordSO : AWeaponSO
{
    public float rotationSpeed; // Velocidad de rotaci�n
    public bool isAttacking = false;
    private Coroutine _rotationCoroutine;

    public override void Attack()
    {
        AudioManager.instance.PlaySwordAudio();

        if (isAttacking) return; // Evita iniciar m�ltiples Coroutines

        if (gameWeapon == null)
        {
            Debug.LogError("gameWeapon no est� asignado.");
            return;
        }

        isAttacking = true;
        Debug.Log("Atacando con " + gameWeapon.name);
        _rotationCoroutine = gameWeapon.GetComponent<MonoBehaviour>().StartCoroutine(RotateWeapon());
    }

    private IEnumerator RotateWeapon()
    {
        while (isAttacking)
        {
            gameWeapon.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public override void StopAttack()
    {
        AudioManager.instance.StopSwordAudio();
        
        if (!isAttacking) return; // No hace nada si no se est� atacando

        isAttacking = false;
        Debug.Log("Deteniendo ataque con " + gameWeapon.name);

        if (_rotationCoroutine != null)
        {
            gameWeapon.GetComponent<MonoBehaviour>().StopCoroutine(_rotationCoroutine);
            _rotationCoroutine = null;
        }

        // Restaura la rotaci�n inicial
        gameWeapon.transform.localRotation = Quaternion.identity;
    }
}
