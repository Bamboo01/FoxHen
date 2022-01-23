using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] CanvasGroup splashCanvasGroup;
    bool loadedScene;
    bool unloadedScene;
    bool hasStartedAsyncLoading = false;
    AsyncOperation operation;

    private void Start()
    {
        loadedScene = false;
        unloadedScene = false;
    }

    void Update()
    {
        if (hasStartedAsyncLoading) return;

        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IPreloadedObject>();
        foreach (IPreloadedObject s in ss)
        {
            if (!s.isDoneLoading)
                return;
        }

        

        //if (!loadedScene)
        //{
        //    operation = SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive);
        //    operation.allowSceneActivation = false;
        //    loadedScene = true;
        //}

        splashCanvasGroup.alpha -= (Time.deltaTime / 3);
        if (splashCanvasGroup.alpha == 0)
        {
            hasStartedAsyncLoading = true;
            StartCoroutine(BeginOperation());
        }

        IEnumerator BeginOperation()
        {
            yield return StartCoroutine(StartLoadingLobbyScene());
            yield return StartCoroutine(StartUnloadingSplashScreen());
        }

        IEnumerator StartUnloadingSplashScreen()
        {
            var asyncUnload = SceneManager.UnloadSceneAsync(gameObject.scene);
            while (!asyncUnload.isDone) yield return null;
            yield break;
        }

        IEnumerator StartLoadingLobbyScene()
        {
            var _asyncSceneLoadOperation = SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive);
            _asyncSceneLoadOperation.allowSceneActivation = false;
            while (!_asyncSceneLoadOperation.isDone)
            {
                if (_asyncSceneLoadOperation.progress >= 0.9f)
                    _asyncSceneLoadOperation.allowSceneActivation = true;
                yield return null;
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("LobbyScene"));
            yield break;
        }
    }

    public void SceneUnloaded(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LobbyScene"));
    }

}
