using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   private Rigidbody2D theRB;
    public float bulletSpeed = 500f;
    public float bulletDuration = 10f;
    
    
    void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    
    void Start()
    {
        
    }

    public void ProjectBullet(Vector2 direction)
    {
        theRB.AddForce(direction * bulletSpeed); 

        Destroy(gameObject, bulletDuration);
        
    }
    //after colliding we want to destroy the obj
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
