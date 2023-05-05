using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame2 : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D flame;
    // Start is called before the first frame update
    void Start()
    {
        flame.velocity = transform.right * speed;
        Physics2D.queriesStartInColliders = false;
        StartCoroutine(FireTimer());
        //Physics2D.IgnoreLayerCollision(9, 6, true);
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
