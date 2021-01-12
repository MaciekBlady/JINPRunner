﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ASingleton<GameController>
{
    private const string GAMEPLAY_SCENE_NAME = "Gameplay";

    private SceneLoader m_SceneLoader;

    private ulong m_Points = 0;

    private bool m_Paused = false;

    public void HandleCoinPickedUp(Coin coin)
    {
        if (coin == null)
        {
            return;
        }

        m_Points += coin.Points;
        Debug.LogFormat("Total points: {0}", m_Points);
    }

    protected override void Initalize()
    {
        m_SceneLoader = new SceneLoader();
        m_SceneLoader.LoadScene(GAMEPLAY_SCENE_NAME, HandleSceneLoaded);
    }

    private void HandleSceneLoaded(Scene loadedScene)
    {
        Debug.LogFormat("Loaded scene {0}", loadedScene.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_Paused = !m_Paused;
            Time.timeScale = m_Paused ? 0f : 1f;
        }
    }
}
