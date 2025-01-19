using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public GameObject enemy1;
    public GameObject enemy2;
    public CanvasGroup backgreoundTutorial;
    public static int numTXT = 1;

    public SwordSO sword;
    public FireSO fire;
    public GunSO gun;

    private void Awake()
    {
        enemy1.SetActive(false);
        enemy2.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            numTXT++;
            if (numTXT == 1)
            {
                textMesh.text = "Usa las teclas W, A, S, D\npara moverte en 8 direcciones";
            }
            else if (numTXT == 2)
            {
                textMesh.text = "Usa las teclas 1, 2 o 3\npara escoger el arnma";
            }
            else if (numTXT == 3)
            {
                textMesh.text = "Click derecho del ratón para atacar";
            }
            else if (numTXT == 4)
            {
                textMesh.text = "Tienes 2 tipos diferentes de enemigos";
            }
            else if (numTXT == 5)
            {
                textMesh.text = "El enemigo explosivo explota";
                enemy1.SetActive(true);
            }
            else if (numTXT == 6)
            {
                textMesh.text = "El enemigo que dispara te dispara";
                enemy2.SetActive(true);

            }
            else if (numTXT == 7)
            {
                textMesh.text = "Los enemigos te darán puntos.\nPuedes canjearlos para mejorar las armas pulsando P";
            }
            else if (numTXT == 8)
            {
                textMesh.text = "Si derretoas podrás avanzar a la ultima habitación";
            }
            else if (numTXT == 9)
            {
                textMesh.text = "¡Buena suerte!";
                NewBehaviourScript.points = 0;
                sword.damage = 15;
                fire.damage = 15;
                gun.damage = 15;
                SceneManager.LoadScene(4);
            }
        }
    }
}
