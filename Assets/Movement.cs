using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlantformerPlayer controller;
    public float speed = 6.5f;

    private float moveHor = 0f;

    private bool jump = false;

    private bool crouch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        moveHor = Input.GetAxis("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            crouch = false;
        }
        
      
        
    }

    private void FixedUpdate()
    {
        controller.Move(moveHor * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
