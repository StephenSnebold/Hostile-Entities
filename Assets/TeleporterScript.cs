using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{

    [SerializeField] public int TeleporterID;

    public LevelSelect controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.UpArrow) && collision.tag == "Player")
        {
            controller.CheckID(TeleporterID);
            controller.ActivateMenu();
            Time.timeScale = 0;
            
            
        }
    }
}
