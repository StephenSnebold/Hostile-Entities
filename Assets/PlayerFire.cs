using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public Transform gunpoint;

    public GameObject fireBallPrefab;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
    }
    
    void Shoot()
    {
        Instantiate(fireBallPrefab, gunpoint.position, gunpoint.rotation);
    }
}
