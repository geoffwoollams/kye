using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public static bool GodMode { get; private set; }
    public static bool UnlimitedLives { get; private set; }
    public static string Message { get; private set; }

    private static float _lastMessageTime;

    public static void TryCheat(string input)
    {
        if(input == "god")
        {
            GodMode = !GodMode;
            SetMessage("God Mode " + (GodMode ? "Enabled!" : "Disabled!"));
        }
        else if(input == "lives")
        {
            UnlimitedLives = !UnlimitedLives;
            SetMessage("Unlimited Lives " + (UnlimitedLives ? "Enabled!" : "Disabled!"));
        }
    }

    private static void SetMessage(string message)
    {
        Message = message;
        _lastMessageTime = Time.time;
    }
}
