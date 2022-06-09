using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D theRB;
    public Sprite[] sprites;
    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 5f;
    public float maxLifetime = 20f;

    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];       //get random sprite

        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);   //to rotate the sprites
        this.transform.localScale =  Vector3.one * this.size;

        theRB.mass = this.size;
    }

    private void Awake(){

        spriteRenderer = GetComponent<SpriteRenderer>();
        theRB = GetComponent<Rigidbody2D>();

    }
 
    public void SetTrajectory(Vector2 direction)
    {
        theRB.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.tag == "Bullet")        //if the enemy is hit
        {
            if(this.size / 2 >= minSize)
            {
                Split();
                Split();
            }
            
            FindObjectOfType<GameManager>().EnemyDestroyed(this);
            Destroy(this.gameObject);
            
        }
        
    }

    private void Split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;     
    
        Enemy half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized); //picking a random direction-we also normalize the speed 

    }


}
