using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMoveing : MonoBehaviour
{
    
    

    private float _speed = 30f;
    

    

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        
    }

   
}
