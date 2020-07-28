using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public string LastLevelPlayed;
    public bool IsFullScreen;
    public int ScreenWidth;
    public int ScreenHeight;

    public Settings(string lastLevelPlayed, int screenWidth, int screenHeight, bool isFullScreen)
    {
        LastLevelPlayed = lastLevelPlayed;
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        IsFullScreen = isFullScreen;
    }
}
