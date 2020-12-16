using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private class LoadSceneTask
    {
        private AsyncOperation m_AsyncOperation;

        private string m_SceneName = null;

        private Action<Scene> m_Callback = null;

        private bool m_SetAsActive = false;

        public Scene LoadedScene;

        public LoadSceneTask(string sceneName, bool setSceneAsActive, Action<Scene> OnSceneLoadedCallback)
        {
            m_SceneName = sceneName;
            m_Callback = OnSceneLoadedCallback;
            m_SetAsActive = setSceneAsActive;

            m_AsyncOperation = SceneManager.LoadSceneAsync(m_SceneName, LoadSceneMode.Additive);
            m_AsyncOperation.completed += HandleAsyncOperationCompleted;
        }

        private void HandleAsyncOperationCompleted(AsyncOperation obj)
        {
            LoadedScene = SceneManager.GetSceneByName(m_SceneName);
            if (m_SetAsActive)
            {
                SceneManager.SetActiveScene(LoadedScene);
            }

            m_Callback.Invoke(LoadedScene);
        }
    }

    private Dictionary<string, Scene> m_LoadedScenes = new Dictionary<string, Scene>();

    private Dictionary<string, LoadSceneTask> m_LoadingTasks = new Dictionary<string, LoadSceneTask>();

    public SceneLoader()
    {
        RegisterLoadedScenes();
    }

    private void RegisterLoadedScenes()
    {
        m_LoadedScenes.Clear();
        int loadedScenesCount = SceneManager.sceneCount;

        for (int i = 0; i < loadedScenesCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!m_LoadedScenes.ContainsKey(scene.name))
            {
                m_LoadedScenes.Add(scene.name, scene);
            }
        }
    }

    public bool IsSceneLoaded(string sceneName) => m_LoadedScenes.ContainsKey(sceneName);

    public void LoadScene(string sceneName, Action<Scene> OnSceneLoadedCallback, bool setSceneAsActive = true)
    {
        if (m_LoadingTasks.ContainsKey(sceneName))
        {
            Debug.LogErrorFormat("Scene is already loading! {0}", sceneName);
            return;
        }

        Scene scene;
        if (m_LoadedScenes.TryGetValue(sceneName, out scene))
        {
            if (setSceneAsActive)
            {
                SceneManager.SetActiveScene(scene);
            }

            OnSceneLoadedCallback?.Invoke(scene);
        }
        else
        {
            LoadSceneTask task = new LoadSceneTask(sceneName, setSceneAsActive, (loadedScene) =>
            {
                m_LoadedScenes.Add(loadedScene.name, loadedScene);
                OnSceneLoadedCallback?.Invoke(loadedScene);

                m_LoadingTasks.Remove(loadedScene.name);
            });

            m_LoadingTasks.Add(sceneName, task);
        }
    }
}

