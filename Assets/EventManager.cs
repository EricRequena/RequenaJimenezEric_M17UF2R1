using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public string enemyTag = "Enemy"; // Etiqueta para identificar a los enemigos.
    public List<GameObject> enemies = new List<GameObject>(); // Lista de enemigos en la escena.
    public List<GameObject> visibleEnemies = new List<GameObject>(); // Lista de enemigos visibles.
    public Camera mainCamera;

    // Eventos para enemigos visibles
    public static event Action OnEnemiesVisible;
    public static event Action OnNoEnemiesVisible;

    // Nuevos eventos para enemigos vivos
    public static event Action OnEnemiesAlive;
    public static event Action OnNoEnemiesAlive;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateEnemyList(); // Actualiza la lista de enemigos
        CheckEnemies();    // Revisa si hay enemigos visibles
        CheckAliveEnemies(); // Revisa si hay enemigos vivos
        CheckVisibleEnemies(); // Revisa enemigos visibles para disparar eventos
    }

    private void UpdateEnemyList()
    {
        enemies.Clear();
        enemies.AddRange(GameObject.FindGameObjectsWithTag(enemyTag));
    }

    public void CheckEnemies()
    {
        visibleEnemies.Clear();

        foreach (GameObject enemy in enemies)
        {
            Renderer renderer = enemy.GetComponent<Renderer>();
            if (renderer != null && renderer.isVisible)
            {
                visibleEnemies.Add(enemy);
            }
        }
    }

    public void CheckVisibleEnemies()
    {
        if (visibleEnemies.Count == 0)
        {
            OnNoEnemiesVisible?.Invoke(); // Dispara el evento cuando no hay enemigos visibles
        }
        else
        {
            OnEnemiesVisible?.Invoke(); // Dispara el evento cuando hay enemigos visibles
        }
    }

    public void CheckAliveEnemies()
    {
        // Comprobar si hay enemigos vivos en la lista
        if (enemies.Count > 0)
        {
            OnEnemiesAlive?.Invoke(); // Dispara el evento cuando hay enemigos vivos
        }
        else
        {
            OnNoEnemiesAlive?.Invoke(); // Dispara el evento cuando no hay enemigos vivos
        }
    }
}
