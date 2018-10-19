using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMDSelector : MonoBehaviour {

	// Use this for initialization
	public GameObject ViveCameraRig;
	public GameObject OculusCamerRig;

	void Start () {
	
		if (UnityEngine.XR.XRDevice.model.Contains("Oculus")) 
		{
			OculusCamerRig.SetActive (true);
		} else
		{
			ViveCameraRig.SetActive (true);
		}
	}
	

}
