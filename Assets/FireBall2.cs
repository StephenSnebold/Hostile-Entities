using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall2 : MonoBehaviour
{
    public float speed = -20f;

    public Rigidbody2D fire;

    // Start is called before the first frame update
    void Start()
    {
        fire.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }
}
