using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public Settings settings;

    private bool _isFullscreen;

    public static AppController Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Go()
    {
        if(Instance != null)
            return;
        
        GameObject appGo = new GameObject();
        appGo.name = "AppController";
        DontDestroyOnLoad(appGo);

        AppController app = appGo.AddComponent<AppController>();
        Instance = app;
    }

    void Awake()
    {
        settings = SaveSystem.Load();
        //if(!settings.IsFullScreen)
        //    Screen.SetResolution(settings.ScreenWidth, settings.ScreenHeight, false);
    }

    void Update()
    {
        if(Screen.fullScreen && !_isFullscreen)
        {
            _isFullscreen = true;
            Save();
        }
        else if(!Screen.fullScreen && _isFullscreen)
        {
            Screen.SetResolution(settings.ScreenWidth, settings.ScreenHeight, false);
            _isFullscreen = false;
            Save();
        }
        else if(!Screen.fullScreen && (Screen.width != settings.ScreenWidth || Screen.height != settings.ScreenHeight))
        {
            settings.ScreenHeight = Screen.height;
            settings.ScreenWidth = Screen.width;
            Save();
        }
    }

    public void Save()
    {
        SaveSystem.Save(settings);
    }
}
