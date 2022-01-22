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

    private void Start()
    {
        loadedScene = false;
    }

    void Update()
    {
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IPreloadedObject>();
        foreach (IPreloadedObject s in ss)
        {
            if (!s.isDoneLoading)
                return;
        }

        if (!loadedScene)
        {
            SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive);
            loadedScene = true;
        }

        splashCanvasGroup.alpha -= ( Time.deltaTime / 3 );
        if (splashCanvasGroup.alpha == 0)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("SplashScreen"));
    }

}
