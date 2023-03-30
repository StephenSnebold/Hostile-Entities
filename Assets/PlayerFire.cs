using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public Transform gunpoint;
    public Transform gunpoint2;

    public GameObject fireBallPrefab;
    public GameObject fireBall2Prefab;
    
    private bool faceRight = true;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && !faceRight) 
        {
            faceRight = !faceRight;
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && faceRight) 
        {
            faceRight = !faceRight;
        }
    }
    
    void Shoot()
    {
        if (faceRight)
        {
            Instantiate(fireBallPrefab, gunpoint.position, gunpoint.rotation);
        }
        else
        {
            Instantiate(fireBall2Prefab, gunpoint2.position, gunpoint2.rotation);
        }
        
    }
    
   
    
}
