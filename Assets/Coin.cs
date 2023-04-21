using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour , IDataPersistance
{
    [SerializeField] private string id;
    
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid() 
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    private SpriteRenderer visual;

    private bool collected = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!collected)
        {
            collectingCoin();
        }
    }

    private void collectingCoin()
    {
        collected = true;
        visual.gameObject.SetActive(false);
        EventSystem.instance.CoinCollected();
        
    }


    public void LoadData(GameData data) 
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if (collected) 
        {
            visual.gameObject.SetActive(false);
        }
    }
    

    public void SaveData(GameData data) 
    {
        if (data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, collected);
    }
    
    
}
