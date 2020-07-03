using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Barrel : MonoBehaviour
{
    
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}