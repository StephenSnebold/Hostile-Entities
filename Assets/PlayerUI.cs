using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeIcon(int heartnum)
    {
        switch (heartnum)
        {
            case 1:
                GameObject.Find("Hearts(3)");
                

                break;
        }
    }
    
    
}
