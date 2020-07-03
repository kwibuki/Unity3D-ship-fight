using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 7.5f;

    void Start()
    {
        Destroy(this.gameObject, 10.0f); //If bullet doesn't collide with other gameObject
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, bulletSpeed), Space.Self);
    }
    void OnCollisionEnter(Collision collision)
    {

        
        if (collision.gameObject.tag == "AI") 
        {

            collision.gameObject.GetComponent<ShipHealth>().TakeDamage();
        }

        Destroy(this.gameObject);
    }
}
