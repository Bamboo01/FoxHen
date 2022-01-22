using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Bamboo.Utility;

public class GameManager : Singleton<GameManager>
{
    List<GameObject> players;

    public enum sceneNames
    {
        MainMenu,
        TempGameplay,
    }

    public sceneNames currScene;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneChange;
    }

    private void SceneChange(Scene scene, LoadSceneMode loadMode)
    {
        currScene = (sceneNames)Enum.Parse(typeof(sceneNames), scene.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
    }

    public void PlayerEnter(PlayerInput input)
    {
        players.Add(input.gameObject);
        if (currScene == sceneNames.MainMenu)
        {
            MainMenuManagerPLUS.Instance.playerEnter(input.gameObject);
        }
    }

    public void PlayerLeave(PlayerInput input)
    {
        players.Remove(input.gameObject);
        if (currScene == sceneNames.MainMenu)
        {
            MainMenuManagerPLUS.Instance.playerLeave(input.gameObject);
        }
    }
}
