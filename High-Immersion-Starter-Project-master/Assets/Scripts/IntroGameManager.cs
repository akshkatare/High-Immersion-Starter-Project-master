using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameManager : MonoBehaviour {

    public int Clicks;
    public static IntroGameManager instance;
    public Canvas[] Panels;
    int currentcanvasCount;

    private void Start()
    {
        instance = this;
    }

    public void LoadNewLevel(string LevelName)
    {
        this.GetComponent<SteamVR_LoadLevel>().levelName = LevelName;
        this.GetComponent<SteamVR_LoadLevel>().Trigger();
    }


    public void HandleCanvas()
    {
        Panels[currentcanvasCount].gameObject.SetActive(false);
        currentcanvasCount++;
        Panels[currentcanvasCount].gameObject.SetActive(true);
    }

}
