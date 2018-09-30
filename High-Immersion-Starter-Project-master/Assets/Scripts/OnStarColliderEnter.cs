using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStarColliderEnter : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            StarCollector.instance.StarCollected();
            this.gameObject.SetActive(false);
        }
    }
}
