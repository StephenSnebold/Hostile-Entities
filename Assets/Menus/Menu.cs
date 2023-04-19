using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
   [Header("First Selected Button")] [SerializeField] private GameObject firstselcted;

   protected void OnEnable()
   {
      StartCoroutine(SetFirstSelected(firstselcted));
   }

   public IEnumerator SetFirstSelected(GameObject firstselctedObject)
   {
      EventSystem.current.SetSelectedGameObject(null);
      yield return new WaitForEndOfFrame();
      EventSystem.current.SetSelectedGameObject(firstselctedObject);
   }
   
}
