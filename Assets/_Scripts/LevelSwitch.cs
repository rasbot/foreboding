using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour {

    public int currLevel;
    public string[] levelNames = { "Intro", "Forboding Manor" };

    // Use this for initialization
    void Start()
    {
        //currLevel = 0;
        currLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void LevelSwitcher()
    {
        SteamVR_LoadLevel.Begin(levelNames[currLevel + 1]);
    }
}
