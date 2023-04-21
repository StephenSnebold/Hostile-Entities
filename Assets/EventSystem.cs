using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    
    public static EventSystem instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        if ( instance != null)
        {
            Debug.LogError("MorethanOneEventSystem");
        }

        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public event Action onCoinCollected;
    public void CoinCollected() 
    {
        if (onCoinCollected != null) 
        {
            onCoinCollected();
        }
    }

    //public event Action health;

}
