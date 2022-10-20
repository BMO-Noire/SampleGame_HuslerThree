using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class JsManager : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void onGameLoaded();
    [DllImport("__Internal")]
    public static extern void onLoadFailed();
    [DllImport("__Internal")]
    public static extern void muteSound();
    [DllImport("__Internal")]
    public static extern void gameStart();
    [DllImport("__Internal")]
    public static extern void gameEnd(string score);
}
