using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosRoom : MonoBehaviour
{

    public GameObject notPassleftLast;
    public GameObject notPassrightLast;
    public GameObject notPasstopLast;
    public GameObject notPassbottomLast;
    public void OnEnable()
    {

        EventManager.OnNoEnemiesAlive += ActivateLastRoom;
        EventManager.OnEnemiesAlive += DesactivateeLastRoom;
    }

    public void OnDisable()
    {
        EventManager.OnNoEnemiesAlive -= ActivateLastRoom;
        EventManager.OnEnemiesAlive -= DesactivateeLastRoom;
    }

    public void ActivateLastRoom()
    {
        notPassleftLast.SetActive(false);
        notPassrightLast.SetActive(false);
        notPasstopLast.SetActive(false);
        notPassbottomLast.SetActive(false);
    }

    public void DesactivateeLastRoom()
    {
        notPassleftLast.SetActive(true);
        notPassrightLast.SetActive(true);
        notPasstopLast.SetActive(true);
        notPassbottomLast.SetActive(true);
    }
}
