using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitBendPipe : MonoBehaviour {
    public float force;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward*force);
           

            // collision.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.up*5f);
        }
    }
}
