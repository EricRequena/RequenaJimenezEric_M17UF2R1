using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    peephole : MonoBehaviour
{
    [SerializeField]
    Transform target;
    

 

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mouseWorldPosition.z = 0;

        Vector3 lookAtDirection = mouseWorldPosition - target.position; 
        target.position = lookAtDirection;

    }
}
