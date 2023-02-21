using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20f;

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
}
