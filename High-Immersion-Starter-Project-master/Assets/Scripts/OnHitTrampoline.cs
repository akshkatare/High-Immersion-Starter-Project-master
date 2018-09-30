using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitTrampoline : MonoBehaviour {

    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
           // collision.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.up*5f);
        }
    }
}
