using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 movementVelocity; 
    private Vector2 offset;
    private Material material;


    private Vector3 lastPlayerPosition; 

    private void Awake()
    {
        
        material = GetComponent<SpriteRenderer>().material;


    }

    private void Update()
    {
      
      

       
        offset = Time.deltaTime * movementVelocity;


        material.mainTextureOffset += offset;

      

    }
}
