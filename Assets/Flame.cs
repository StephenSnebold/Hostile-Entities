using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float speed = -5f;

    public Rigidbody2D flame;
    // Start is called before the first frame update
    void Start()
    {
        flame.velocity = transform.right * speed;
        Physics2D.queriesStartInColliders = false;
        //Physics2D.IgnoreLayerCollision(9, 6, true);
        StartCoroutine(FireTimer());
        Debug.Log("Fired Flame");
    }
    
    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(10f);
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "turnAround")
        {
            Debug.Log("FlameDestroyed");
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "turnAround")
        {
            Destroy(gameObject);
        }
    }
}
