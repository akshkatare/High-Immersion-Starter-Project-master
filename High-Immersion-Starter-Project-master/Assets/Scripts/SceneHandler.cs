using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour {

    public string thisLevel;
    public string NextLevel;
    public static SceneHandler instance;

    private void Start()
    {
        instance = this;
    }
    public void LoadThisLevel()
    {
        this.GetComponent<SteamVR_LoadLevel>().levelName=thisLevel;
        this.GetComponent<SteamVR_LoadLevel>().Trigger();

    }
    public void LoadNextLevel()
    {
        this.GetComponent<SteamVR_LoadLevel>().levelName = NextLevel;
        this.GetComponent<SteamVR_LoadLevel>().Trigger();
    }
}
