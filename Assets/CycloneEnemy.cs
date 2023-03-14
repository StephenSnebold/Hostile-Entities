using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneEnemy : MonoBehaviour
{
    public int health = 100;
    
    private float speed = 3;
    private Animator anim;
    private Rigidbody2D rb2d;
    bool faceRight = false;
    private bool walking;
    private int direction = -1;

    public GameObject deathEffect;

    public void TakeDamage(int damage)
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
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        StartCoroutine(CycloneAI());

    }

    IEnumerator CycloneAI()
    {
        rb2d.velocity = new Vector2(speed* direction, rb2d.velocity.y);
        
        yield return new WaitForSeconds(5);

        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        anim.SetBool("Fire", true);
        //spout fire
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
     
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            TakeDamage(10);
        }
        
        if (col.tag == "Wall")
        {
            direction *= -1;
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
     
     
     
}
