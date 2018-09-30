using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    Vector3 originalPosition;
    public static ResetPosition instance;
	// Use this for initialization
	void Start () {
        instance = this;
        originalPosition = this.transform.position;
        this.GetComponent<Rigidbody>().isKinematic = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (StarCollector.instance.numberOfStarsCollected == StarCollector.instance.starsToWin)
            {
                SceneHandler.instance.LoadNextLevel();
            }
            else
            {
                ResetBallPosition();
            }
        }
    }


    public void ResetBallPosition()
    {
        this.transform.position = originalPosition;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        this.GetComponent<Rigidbody>().isKinematic = true;

        StarCollector.instance.ResetStars();
        
    }
}
