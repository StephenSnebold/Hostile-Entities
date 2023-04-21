using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{

    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;

    [Header("File Storage Config")] 
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncyption;
    
    
    
    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObjects;

    private FileDataHandler dataHander;

    private string selectedProfileId = "";
    public static DataPersistanceManager instance { get; private set;}
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one instance in the scene");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);


        this.dataHander = new FileDataHandler(Application.persistentDataPath, fileName, useEncyption);
        this.selectedProfileId = dataHander.GetMostRecentlyUpdatedProfileId();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        
        LoadGame();
    }
    
    

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        
        if (disableDataPersistence) 
            {
                return;
            }
    
    
        this.gameData = dataHander.Load(selectedProfileId);
        if (this.gameData == null)
        {
            Debug.Log("No data was found, New Game needs to be started");
            NewGame();
            return;
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
        
    }

    public void SaveGame()
    {
    
        if (disableDataPersistence) 
        {
            return;
        }

        

        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. Start a New Game");
            return;
        }
        
        
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(gameData);
        }

        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        Scene scene = SceneManager.GetActiveScene();

        if (!scene.name.Equals("Main Menu"))
        {
            gameData.currentScene = scene.name;
        }
        
        dataHander.Save(gameData, selectedProfileId);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects =
            FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHander.LoadAllProfiles();
    }

    public bool HasData()
    {
        return gameData != null;
    }

    public string GetSavedScene()
    {
        if (gameData == null)
        {
            return null;
        }

        return gameData.currentScene;
    }

}
