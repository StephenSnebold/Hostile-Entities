using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class wormFireball : MonoBehaviour
{
    public Rigidbody2D body;
    private Vector3 reflectDir;

    private float moveSpeed = 3.8f;

    //public LayerMask CollisionMask;

    public Transform wormFire;
    public GameObject nextFireball;
    public GameObject nextFireball2;

    public int FireTag;
    
    

    private Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        switch (FireTag)
        {
            case 1:
                body.velocity = new Vector2(-3.4f, 3.4f);//left-up
                break;
            case 2:
                body.velocity = new Vector2(3.4f, 3.4f);//right-up
                break;
            case 3:
                body.velocity = new Vector2(3.4f, -3.4f);//right-down
                break;
            
            case 4:
                body.velocity = new Vector2(-3.4f, -3.4f);//left-down
                break;

        }
       
        Physics2D.SyncTransforms();
        Physics2D.reuseCollisionCallbacks = true;
        Physics2D.queriesHitTriggers = true;
        Physics2D.queriesStartInColliders = false;
        StartCoroutine(FireTimer());
    }

    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(10f);
        
        Destroy(gameObject);
    }
    /*
    
       private void OnCollisionEnter2D(Collision2D col)
       {
           if (col.gameObject.CompareTag("Wall"))
           {
               Vector2 wallnormal = col.contacts[0].normal;
               reflectDir = Vector2.Reflect(body.velocity, wallnormal);
   
               body.velocity = reflectDir * body.velocity;
           }
       }
       */

  
       private void OnTriggerEnter2D(Collider2D col)
       {
           /*
          if (col.CompareTag("down") || col.CompareTag("up"))
          {
              
              Instantiate(nextFireball, wormFire.position, wormFire.rotation);
              Destroy(gameObject);
          }

          if (col.CompareTag("right") || col.CompareTag("left"))
          {
              Instantiate(nextFireball2, wormFire.position, wormFire.rotation);
              Destroy(gameObject);
          }
          */
        
          Vector3 currentScale = gameObject.transform.localScale;
  
          if (col.CompareTag("down"))
          {
              body.velocity = new Vector2( body.velocity.x,-3.4f);
                  
              currentScale.y *= -1;
                  
              gameObject.transform.localScale = currentScale;
  
              Debug.Log(body.velocity.x);
              Debug.Log(body.velocity.y);
          }
  
          if (col.CompareTag("right"))
          {
              body.velocity = new Vector2( 3.4f,body.velocity.y );
                  
              currentScale.x *= -1;
              gameObject.transform.localScale = currentScale;
                  
              Debug.Log(body.velocity.x);
              Debug.Log(body.velocity.y);
          }
  
          if (col.CompareTag("left"))
          {
              body.velocity = new Vector2( -3.4f,body.velocity.y );
                  
              currentScale.x *= -1;
              gameObject.transform.localScale = currentScale;
                  
              Debug.Log(body.velocity.x);
              Debug.Log(body.velocity.y);
          }
  
          if (col.CompareTag("up"))
          {
              body.velocity = new Vector2( body.velocity.x,3.4f);
                  
              currentScale.y *= -1;
              gameObject.transform.localScale = currentScale;
                  
              Debug.Log(body.velocity.x);
              Debug.Log(body.velocity.y);
          }
          /*
          switch (col.tag)
          {
              case "down":
                  
  
                  
                  break;
              
              case "right":
                  
                  
                  break;
              
              case "left":
                  
                  
                  
                  break;
              
              case "up":
                  
                  break;
              
              default:
                  
                  break;
              
          }
             */
       }


    // Update is called once per frame
    void Update()
    {
        /*
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        
        wormFire.Rotate(Vector3.up * moveSpeed * Time.deltaTime);
        //body.velocity = lastVelocity * moveSpeed;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * moveSpeed + .1f, CollisionMask))
        {
            Vector3 reflectDir = Vector2.Reflect(ray.direction, hit.normal);
            float rot = Mathf.Atan2(reflectDir.x, reflectDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0,rot, 0);
        }
        */

    }
    
}
