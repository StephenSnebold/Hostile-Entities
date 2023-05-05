using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HofarEnemy : MonoBehaviour
{
    public int health = 100;
    public float verSpeed;
    public float horSpeed;
    private int direction = -1;

    public Transform Flameport1;
    public Transform Flameport2;

    public GameObject Flame;
    public GameObject Flame2;

    [SerializeField] private Collider2D normal;
    [SerializeField] private Collider2D facedown;

    public GameObject deathEffect;

    private Animator anim;
    private Rigidbody2D body;

    [SerializeField] private Transform player;

    [SerializeField] float agroRange = 11;
    
    
    private bool detectPlayer;
    private bool hitground;
    private bool backToOrgin;
    
    
    bool faceRight = false;
    
    public SpriteRenderer sprite;

    //[SerializeField] private GameObject Hofar;

    //[SerializeField] private GameObject HofarDetect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        horSpeed = 3;
        verSpeed = 0;

        normal.enabled = true;
        facedown.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
        body.velocity = new Vector2(horSpeed * direction, verSpeed);

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            agroRange = 0;
            StartCoroutine(playerIsDetected());
        }

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
    
    IEnumerator Hofarhit()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    //IEnumerator DisablePlayerDetection()
    //{
    //    agroRange = 0;
   //     yield return new WaitForSeconds(0.5f);
   //     agroRange = 11;
   // }

    IEnumerator Hofarhitground()
    {
        
        yield return new WaitForSeconds(1f);
        

        hitground = false;
        backToOrgin = true;
        
        anim.SetBool("HitGround", false);
        
        yield return new WaitForSeconds(.1f);

        //normal.enabled = true;
        //facedown.enabled = false;
      

        verSpeed = 4.8f;



    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "bullet")
        {
            TakeDamage(10);
            StartCoroutine(Hofarhit());
        }
        
        
        
        if (col.tag == "Wall" && detectPlayer == false || col.tag == "turnAround" && detectPlayer == false)
        {
            direction *= -1;
            
            //flip();
            
        }

        if (col.tag == "Wall" && detectPlayer == true)
        {
            hitground = true;
            Debug.Log("The enemy hit the ground");
            Instantiate(Flame, Flameport1.position, Flameport1.rotation);
            Instantiate(Flame2, Flameport2.position, Flameport2.rotation);
            anim.SetBool("HitGround", true);
            anim.SetBool("DetectPlayer", false);

            verSpeed = 0;
            detectPlayer = false;


            StartCoroutine(Hofarhitground());


        }
        
        
        if (col.tag == "stop" && backToOrgin == true)
        {
            anim.SetBool("BackToOrginalPosition", true);
            verSpeed = 0;
            horSpeed = 3;
            //anim.SetBool("BackToOrginalPosition", false);
            Debug.Log(detectPlayer);
            //agroRange = 11;
        }

        if (col.tag == "stopDetection")
        {
           // DisablePlayerDetection();
           agroRange = 0;
        }
        

        /*
        
        void flip()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            //currentScale.y *= 1;
  
            gameObject.transform.localScale = currentScale;
  
            faceRight = !faceRight;
  
  
        }
        */
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "stopDetection")
        {
            agroRange = 11;
        }
    }

    IEnumerator playerIsDetected()//Hofar does this when player is detected
    {
        Debug.Log("player detected");
       
        detectPlayer = true;
        anim.SetBool("DetectPlayer", true);
        
        horSpeed = 0;

        yield return new WaitForSeconds(1.5f);

        normal.enabled = false;
        facedown.enabled = true;


        
        verSpeed = -9.8f;
        
        
        
        

    }
    
    
}
