using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour, IDataPersistance
{
    // Start is called before the first frame update
    private bool unlockLevel3;
    private int newTeleporter;

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelOneClicked()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        newTeleporter = 0;
        DataPersistanceManager.instance.SaveGame();
        if (scene.name.Equals("SampleScene"))
        {
            DeactivateMenu();
        }
        else
        {
            DeactivateMenu();
            SceneManager.LoadSceneAsync("SampleScene");
        }
    }

    public void OnLevelTwoEntranceClicked()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        newTeleporter = 1;
        DataPersistanceManager.instance.SaveGame();
        if (scene.name.Equals("HostileEntities_Level2"))
        {
            DeactivateMenu();
        }
        else
        {
            DeactivateMenu();
            SceneManager.LoadSceneAsync("HostileEntities_Level2");
        }
    }

    public int Teleporting()
    {
        return newTeleporter;
    }
    
    public void OnLevelTwoExitClicked()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        newTeleporter = 2;
        DataPersistanceManager.instance.SaveGame();
        if (scene.name.Equals("HostileEntities_Level2"))
        {
            DeactivateMenu();
        }
        else
        {
            DeactivateMenu();
            SceneManager.LoadSceneAsync("HostileEntities_Level2");
        }
    }
    
    public void CheckID(int ID)
    {
        if (ID == 2)
        {
            unlockLevel3 = true;
        }
    }

    public void LoadData(GameData data)
    {
        unlockLevel3 = data.unlockedLevel3;
        newTeleporter = data.Teleporter;
    }

    public void SaveData(GameData data)
    {
        data.unlockedLevel3 = unlockLevel3;
        data.Teleporter = newTeleporter;
    }


    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DataPersistanceManager.instance.SaveGame();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

   
    
    
}
