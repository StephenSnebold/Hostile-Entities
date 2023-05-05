using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class CycloneEnemy : MonoBehaviour
{
    public int health = 100;
    private bool loop = true;
    public float speed = 3;
    private Animator anim;
    private Rigidbody2D rb2d;


    bool faceRight = false;
    private bool walking;
    private int direction = -1;
    private bool changediretion;
    private int numshots;
    public Transform fireplace;
    public GameObject Cyclonefire;
    public GameObject Cyclonefire2;
    public float timeBewteenShots;

    public SpriteRenderer sprite;

    public Fire controller;


    public GameObject deathEffect;

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
        controller.KillProjectile();
        Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timeBewteenShots = 34;
        



    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        StartCoroutine(CycloneAI());
        
        //timeBewteenShots = 34;
    }

    IEnumerator Cyclonehit()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    IEnumerator CycloneAI()
    {
       
            speed = 3;
            anim.SetBool("Fire", false);
            //rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);

            yield return new WaitForSeconds(8);



            speed = 0;
            //rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
            anim.SetBool("Fire", true);
            yield return new WaitForSeconds(2);


            timeBewteenShots = 34;

            //spout fire
            if (timeBewteenShots >= 34)
            {
                timeBewteenShots -= Time.deltaTime;
                //timeBewteenShots -= Time.deltaTime;
                if (!faceRight)
                {
                    //Vector2 currentScale = gameObject.transform.localScale;
                   var Fire = Instantiate(Cyclonefire, fireplace.position, fireplace.rotation);
                   Fire.transform.parent = gameObject.transform;
                   Physics2D.IgnoreLayerCollision(9, 0, true);
                   Physics2D.IgnoreLayerCollision(9, 6, true);
                   

                }
                else if (faceRight)
                {
                   var Fire2 = Instantiate(Cyclonefire2, fireplace.position, fireplace.rotation);
                   Fire2.transform.parent = gameObject.transform;
                   Physics2D.IgnoreLayerCollision(9, 0, true);
                   Physics2D.IgnoreLayerCollision(9, 6, true);
                   
                }
                

                yield return new WaitForSeconds(8);
            

                StartCoroutine(CycloneAI());

            //timeBewteenShots = 64;


            }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
     
        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);

        
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
     
     
     
}
