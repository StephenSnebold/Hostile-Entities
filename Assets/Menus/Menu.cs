using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   [Header("First Selected Button")] 
   [SerializeField] private Button firstselcted;

   protected virtual void OnEnable()
   {
      SetFirstSelected(firstselcted);
   }

/*
   public IEnumerator SetFirstSelected(GameObject firstselctedObject)
   {
      EventSystem.current.SetSelectedGameObject(null);
      yield return new WaitForEndOfFrame();
      EventSystem.current.SetSelectedGameObject(firstselctedObject);
   }
   */

   public void SetFirstSelected(Button firstselectedButton) 
   {
      firstselectedButton.Select();
   }
   
}
