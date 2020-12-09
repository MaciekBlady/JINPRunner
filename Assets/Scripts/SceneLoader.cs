using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public LoadSceneTask(string sceneName, Action<Scene> OnSceneLoadedCallback, bool setSceneAsActive = true)
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

    public void LoadScene(string sceneName, Action<Scene> OnSceneLoadedCallback, bool setSceneAsActive = true)
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        AsyncOperation loadSceneOperation =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        throw new NotImplementedException();
        SceneManager.SetActiveScene(scene);

    }
}

