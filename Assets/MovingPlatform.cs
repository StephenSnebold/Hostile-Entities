using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] public GameObject[] waypoints;

    private Transform Mincart;

    public Transform Goal;

    private int currentwaypoint = 1;
    public float speed = 6f;

    private void Start()
    {
        Goal = waypoints[0].transform;
    }

    void Update()
    {
        if(Vector2.Distance(waypoints[currentwaypoint].transform.position, transform.position) < .1f)
        {
            transform.position = Goal.position;
           
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentwaypoint].transform.position, Time.deltaTime*speed);
    }

   

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player" && col.GetContact(0).normal.y==-1)
        {
            col.gameObject.transform.SetParent(transform);
            
        }

        
        
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(null);
        }
        
    }
    // Start is called before the first frame update
    /*public Vector3 finishPos = Vector3.zero;
    public float speed = 0.5f;

    private Vector3 startPos;
    private float trackPercent = 0;
    private int direction = 1;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x, y, startPos.z);

        if ((direction == 1 && trackPercent > .9f) || (direction == -1 && trackPercent < .1f))
        {
            direction *= -1;
        }
        
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, finishPos);
    }
    */
}

