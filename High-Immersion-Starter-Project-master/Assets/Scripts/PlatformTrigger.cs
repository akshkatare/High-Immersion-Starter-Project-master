using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {


    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.layer+" Enter");
        AntiCheat.instance.isPlayerIn = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.layer+" Exit");

        AntiCheat.instance.isPlayerIn = false;
    }

}
