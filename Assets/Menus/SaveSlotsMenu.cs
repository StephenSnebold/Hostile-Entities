using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
   [Header("Menu Navigation")] [SerializeField] private MainMenu mainMenu;
   
  

   [Header("Menu Buttons")] [SerializeField] private Button backButton;

   
   private SaveSlot[] saveSlots;

   private bool isLoadingGame = false;
   private void Awake()
   {
      saveSlots = this.GetComponentsInChildren<SaveSlot>();
   }


   public void ActivateMenu(bool isLoadingGame)
   {
      this.gameObject.SetActive(true);

      this.isLoadingGame = isLoadingGame;
      
      Dictionary<string, GameData> profilesGameData = DataPersistanceManager.instance.GetAllProfilesGameData();

      GameObject firstSelected = backButton.gameObject;
      foreach (SaveSlot saveSlot in saveSlots)
      {
         GameData profileData = null;
         profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
         saveSlot.SetData(profileData);

         if (profileData == null && isLoadingGame)
         {
            saveSlot.SetInteractable(false);
         }
         else
         {
            saveSlot.SetInteractable(true);
            if (firstSelected.Equals(backButton.gameObject))
            {
               firstSelected = saveSlot.gameObject;
            }
         }
         
      }
   }

   public void DeactivateMenu()
   {
      this.gameObject.SetActive(false);
   }

   public void OnSaveSlotClicked(SaveSlot saveSlot)
   {
      DisableMenuButtons();
      
      DataPersistanceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());

      if (!isLoadingGame)
      {
         DataPersistanceManager.instance.NewGame();
      }
      

      SceneManager.LoadSceneAsync("SampleScene");

   }

   public void OnBackClicked()
   {
      mainMenu.ActivateMenu();
      this.DeactivateMenu();
   }

   void DisableMenuButtons()
   {
      foreach (SaveSlot saveSlot in saveSlots)
      {
         saveSlot.SetInteractable(false);
      }

      backButton.interactable = false;
   }

}
