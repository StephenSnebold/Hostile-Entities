using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
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
            Debug.LogError("Found more than one instance in the scene");
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
        this.gameData = dataHander.Load(selectedProfileId);
        
        if (this.gameData == null)
        {
            Debug.Log("No data was found, New Game needs to be started");
            //NewGame();
            return;
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.loadData(gameData);
        }
        
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. Start a New Game");
            return;
        }
        
        
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.saveData(gameData);
        }

        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        dataHander.Save(gameData, selectedProfileId);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

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

}
