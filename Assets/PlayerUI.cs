using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IDataPersistance
{
    public TextMeshProUGUI healthText;

    private int totalhealth;
    
    [SerializeField] private int totalCoins = 0;

    private int coinsCollected = 0;

    public TextMeshProUGUI coinsCollectedText;
    
    public void LoadData(GameData data) 
    {
        foreach(KeyValuePair<string, bool> pair in data.coinsCollected) 
        {
            if (pair.Value) 
            {
                coinsCollected++;
            }
        }
    }
    
    public void SaveData(GameData data)
    {
        // no data needs to be saved for this script
    }

   // private void Awake() 
   // {
   //     coinsCollectedText = this.GetComponent<TextMeshProUGUI>();
   // }
    
    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.instance.onCoinCollected += OnCoinCollected;
    }

    // Update is called once per frame
    

    private void OnDestroy()
    {
        EventSystem.instance.onCoinCollected -= OnCoinCollected;
    }
    
    private void OnCoinCollected() 
    {
        coinsCollected++;
    }


    public void setMaxHealth(int healthDisplay)
    {
        totalhealth = healthDisplay;
    }
    
    
    private void Update()
    {
        coinsCollectedText.text = coinsCollected + "/" + totalCoins;
        healthText.text = "Health : " + totalhealth;
    }
    
    
}
