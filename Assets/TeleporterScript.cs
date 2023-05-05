using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{

    [SerializeField] public int TeleporterID;

    //public LevelSelect controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (TeleporterID)
        {
            case 0:
                SceneManager.LoadScene("HositleEntities_Level2");
                break;
            case 1:
                SceneManager.LoadScene("HE_Level3");
                break;
            case 2:
                SceneManager.LoadScene("SampleScene");
                break;
        }
        
        if (Input.GetKey(KeyCode.UpArrow) && collision.tag == "Player")
        {
            //controller.CheckID(TeleporterID);
            //controller.ActivateMenu();
            //Time.timeScale = 0;
            
            
        }
    }
}
