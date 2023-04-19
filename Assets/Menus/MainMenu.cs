using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigation")] [SerializeField] private SaveSlotsMenu SaveSlotsMenu;
    
    
    [Header("Menu Buttons")] 
    [SerializeField] private Button newGameButton;

    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start()
    {
        if (!DataPersistanceManager.instance.HasData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        SaveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
        //DataPersistanceManager.instance.NewGame();
       // SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnLoadGameClicked()
    {
        SaveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        DataPersistanceManager.instance.SaveGame();
        
        SceneManager.LoadSceneAsync("SampleScene");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
