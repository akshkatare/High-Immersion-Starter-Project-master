using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
     //  Debug.Log(other.gameObject.layer+" Enter");
		if(other.gameObject.layer==11)
        AntiCheat.instance.isPlayerIn = true;
    }
    private void OnTriggerExit(Collider other)
    {
       //Debug.Log(other.gameObject.layer+" Exit");
		if(other.gameObject.layer==11)
        AntiCheat.instance.isPlayerIn = false;
    }

}
