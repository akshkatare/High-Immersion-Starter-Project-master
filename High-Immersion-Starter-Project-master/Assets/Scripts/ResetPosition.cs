using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    Vector3 originalPosition;
	// Use this for initialization
	void Start () {
        originalPosition = this.transform.position;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            this.transform.position = originalPosition;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
