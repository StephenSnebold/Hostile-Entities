using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VolcanicWorm : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    private Animator anim;
    private Rigidbody2D rb2d;
    
    private int direction = 1;
    private bool changediretion;
    bool faceRight;
    public SpriteRenderer sprite;

    public Transform firepoint;

    public GameObject Wormfire;
    public GameObject Wormfire2;
    
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        StartCoroutine(WormAI());
        
        //timeBewteenShots = 34;
    }
    
    IEnumerator Cyclonehit()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    
    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        
    }
    
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "bullet")
        {
            TakeDamage(10);
            StartCoroutine(Cyclonehit());
        }
        
        
        
        if (col.tag == "Wall"  || col.tag == "turnAround")
        {
            direction *= -1;
            changediretion = true;
            flip();
            
        }
        
        void flip()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            //currentScale.y *= 1;
  
            gameObject.transform.localScale = currentScale;
  
            faceRight = !faceRight;
  
  
        }
        
    }

    IEnumerator WormAI()
    {
        speed = -.9f;
        anim.SetBool("Shoot", false);

        yield return new WaitForSeconds(4f);

        speed = 0;
        anim.SetBool("Shoot", true);

        if (!faceRight)
        {
            Instantiate(Wormfire, firepoint.position, firepoint.rotation);

        }
        else
        {
            Instantiate(Wormfire2, firepoint.position, firepoint.rotation);
        }
       
        yield return new WaitForSeconds(.6f);
        
        if (!faceRight)
        {
            Instantiate(Wormfire, firepoint.position, firepoint.rotation);

        }
        else
        {
            Instantiate(Wormfire2, firepoint.position, firepoint.rotation);
        }

        yield return new WaitForSeconds(.6f);
        
        if (!faceRight)
        {
            Instantiate(Wormfire, firepoint.position, firepoint.rotation);

        }
        else
        {
            Instantiate(Wormfire2, firepoint.position, firepoint.rotation);
        }

        yield return new WaitForSeconds(.6f);

        StartCoroutine(WormAI());






    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);

    }

}
