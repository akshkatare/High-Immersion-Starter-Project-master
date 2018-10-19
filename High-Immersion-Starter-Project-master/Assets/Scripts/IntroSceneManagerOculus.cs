using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManagerOculus : MonoBehaviour {

 /*   private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;*/

    private void Start()
    {
        //trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {

            if (IntroGameManager.instance.Clicks < 1)
            {
                IntroGameManager.instance.Clicks++;
                IntroGameManager.instance.HandleCanvas();
            }
            else
            {
                IntroGameManager.instance.LoadNewLevel("Level 1");
            }
        }
    }
}
