using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheat : MonoBehaviour {

    public bool isPlayerIn;
    public static AntiCheat instance;

    private void Start()
    {
        instance = this;
    }
    public void Cheatchecker()
    {
        if (!isPlayerIn)
        {
            Debug.Log("Cheater");
            ResetPosition.instance.ResetBallPosition();
        }
    }
}
