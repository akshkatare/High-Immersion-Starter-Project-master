using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControllerInputManager : MonoBehaviour {

    /*#region Steam VR variables
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId menu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    private Valve.VR.EVRButtonId touchButtonUp = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    private Valve.VR.EVRButtonId touchButtonDown = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    private Valve.VR.EVRButtonId touchButtonRight = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    private Valve.VR.EVRButtonId touchButtonLeft = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    #endregion*/


	private OVRInput.Controller controller;
   
    #region Teleport

    private LineRenderer laser;
    public GameObject Aim;
    public Vector3 TargetLocation;
    public GameObject Player;
    public LayerMask layer;

    public Material laserValidColor;
    public Material LaserInvalidColor;

    #endregion


    #region ObjectSelector

    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;

    #endregion

    #region Variables
    public bool isLeft;
    #endregion

    void Start()
    {
       // trackedObj = GetComponent<SteamVR_TrackedObject>();
        
		if (isLeft)
			controller = OVRInput.Controller.LTouch;
		else
			controller = OVRInput.Controller.RTouch;

		if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
        {
           // touchLast = controller.GetAxis(touchPad).x;
			Vector2 vr=OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);    

			touchLast = vr.x;    
			}

       if (isLeft)
            laser = GetComponentInChildren<LineRenderer>();
           
    }

    // Update is called once per frame
    void Update () {

          #region  CodeForTeleport(LeftHandOnly)
         if (isLeft)
         {
			if (OVRInput.Get(OVRInput.Button.DpadDown))
             {
                 laser.gameObject.SetActive(true);
                 Aim.SetActive(true);
                 laser.SetPosition(0, gameObject.transform.position);

                 RaycastHit hit;
                 if (Physics.Raycast(transform.position, transform.forward, out hit, 15, layer))
                 {

                    if (hit.transform.gameObject.layer == 9)
                    {
                        TargetLocation = hit.point;
                        laser.SetPosition(1, TargetLocation);
                        Aim.transform.position = TargetLocation;
                        laser.material = laserValidColor;
                    }
                    if (hit.transform.gameObject.layer == 10)
                    {
                        laser.material = LaserInvalidColor;
                        laser.SetPosition(1, hit.point);
                        TargetLocation = new Vector3(transform.position.x, 0f, transform.position.z);
                        Aim.transform.position =new Vector3(transform.position.x,0f,transform.position.z);
                    }

                }
                 else
                 {

                     TargetLocation = transform.position + 15 * transform.forward;
                     RaycastHit groundRay;
                     if (Physics.Raycast(TargetLocation, -Vector3.up, out groundRay, 17, layer))
                     {
                         TargetLocation.y = groundRay.point.y;
                     }

                     laser.SetPosition(1, TargetLocation);
                     Aim.transform.position = TargetLocation;
                 }
             }
			if (OVRInput.GetUp(OVRInput.Button.DpadDown))
             {
                 laser.gameObject.SetActive(false);
                 Aim.SetActive(false);
                 Player.transform.position = TargetLocation;
             }
         }
        #endregion


        #region CodeForObjectSpawnerMenu(RightHandOnly)
        if (!isLeft)
        {

			if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
            {
              ///  Debug.Log("RightTouch");
				if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    ObjectMenuManager.instance.SpawnCurrentObject();
                }
				Vector2 vr=OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);   
				touchCurrent = vr.x;
                distance = touchCurrent - touchLast;
                touchLast = touchCurrent;
                swipeSum += distance;
                
                if (!hasSwipedRight)
                {
                    if (swipeSum > 0.5f)
                    {
                        swipeSum = 0;
                        SwipeRight();
                        hasSwipedRight = true;
                        hasSwipedLeft = false;
                    }
                }
                if (!hasSwipedLeft)
                {
                    if (swipeSum < -0.5f)
                    {
                        swipeSum = 0;
                        SwipeLeft();
                        hasSwipedRight = false;
                        hasSwipedLeft = true;
                    }
                }
            }

			if (OVRInput.GetUp(OVRInput.Button.DpadDown))
            {
                ObjectMenuManager.instance.Disable();
                swipeSum = 0;
                touchCurrent = 0;
                touchLast = 0;
                hasSwipedLeft = false;
                hasSwipedRight = false;
            }
        }
        #endregion


		if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            SceneHandler.instance.LoadThisLevel();
        }
    }

    private void SwipeLeft()
    {
        ObjectMenuManager.instance.MenuLeft();
        Debug.Log("Swiped Left");
    }

    private void SwipeRight()
    {
        ObjectMenuManager.instance.MenuRight();
        Debug.Log("Swiped Right");
    }




    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
			if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ReleaseObjectBall(other.gameObject);
            }
			if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                GrabObjectBall(other.gameObject);
            }
        }

        if (other.tag == "Rube objects")
        {
			if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                ReleaseObjectRube(other.gameObject);
            }
			if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                GrabObjectRube(other.gameObject);
            }
        }
    }

    void ReleaseObjectBall(GameObject obj)
    {
        AntiCheat.instance.Cheatchecker();
        obj.transform.SetParent(null);
        obj.GetComponent<Rigidbody>().isKinematic = false;
		obj.GetComponent<Rigidbody> ().velocity = OVRInput.GetLocalControllerVelocity(controller) * 2f;
		obj.GetComponent<Rigidbody>().angularVelocity =  OVRInput.GetLocalControllerAngularVelocity(controller) * 2f;
    }
    void GrabObjectBall(GameObject obj)
    {
        obj.transform.SetParent(gameObject.transform);
        obj.GetComponent<Rigidbody>().isKinematic = true;
        //controller.TriggerHapticPulse(2000);
    }

    void ReleaseObjectRube(GameObject obj)
    {
        obj.transform.SetParent(null);
    }
    void GrabObjectRube(GameObject obj)
    {
        obj.transform.SetParent(gameObject.transform);
        //controller.TriggerHapticPulse(2000);
    }

}
