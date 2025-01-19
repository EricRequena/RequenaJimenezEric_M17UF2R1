using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image barraDeVida;

    private NewBehaviourScript player; 
    void Start()
    {
        player = FindObjectOfType<NewBehaviourScript>(); 
    }

    void Update()
    {
        if (player != null)
        {
            barraDeVida.fillAmount = (float)player.health / 300; 
        }
    }
}
