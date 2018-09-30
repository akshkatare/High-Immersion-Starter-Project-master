using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollector : MonoBehaviour {


    public static StarCollector instance;
    public int numberOfStarsCollected;
    public int starsToWin;

    public GameObject[] Stars;


    public void ResetStars()
    {
        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].SetActive(true);
        }
        numberOfStarsCollected = 0;
    }

    private void Start()
    {
        instance = this;
        numberOfStarsCollected = 0;
    }

    public void StarCollected()
    {
        numberOfStarsCollected++;
        Debug.Log(" Star Collected :" +numberOfStarsCollected);
    }
}
