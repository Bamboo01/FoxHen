using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    void Update()
    {

        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IPreloadedObject>();
        foreach (IPreloadedObject s in ss)
        {
            if (!s.isDoneLoading)
                return;
        }

        SceneManager.LoadScene("Gameplay");
    }
}
