using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody2D theRB;

    private bool moveForward;
    private float turnDirection;
    public float movingSpeed = 1f;
    public float turningSpeed = 1f;
    public Bullet bullet;
    public float respawnTime = 3f;
    public float invulnerabilityTime = 3f;

    void Start()
    {
       // instance=this;
    }

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else{
            turnDirection = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
  
    private void FixedUpdate()
    {
        if(moveForward)
        {
            theRB.AddForce(transform.up * movingSpeed);
        }
        if(turnDirection != 0f)
        {
            theRB.AddTorque(turnDirection * turningSpeed);
        }
    }
    

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bullet, transform.position, transform.rotation);
        bullet.ProjectBullet(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            theRB.velocity = Vector3.zero;
            theRB.angularVelocity = 0f; 

            gameObject.SetActive(false);
            
            FindObjectOfType<GameManager>().PlayerDeath(this);
        }
    }

    private void OnEnable()
    {
        // Turn off collisions for a few seconds after spawning to ensure the player has enough time to safely move away from enemies
        gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Invoke(nameof(TurnOnCollisions), invulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    
}
