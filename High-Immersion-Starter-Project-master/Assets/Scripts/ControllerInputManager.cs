﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

    #region Steam VR variables
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
    #endregion

   
    #region Teleport

    private LineRenderer laser;
    public GameObject Aim;
    public Vector3 TargetLocation;
    public GameObject Player;
    public LayerMask layer;

    public Material laserValidColor;
    public Material LaserInvalidColor;

    #endregion

    #region Variables
    public bool isLeft;
    #endregion

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

       if (isLeft)
            laser = GetComponentInChildren<LineRenderer>();
           
    }

    // Update is called once per frame
    void Update () {

          #region  CodeForTeleport(LeftHandOnly)
         if (isLeft)
         {
             if (controller.GetPress(touchPad))
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
             if (controller.GetPressUp(touchPad))
             {
                 laser.gameObject.SetActive(false);
                 Aim.SetActive(false);
                 Player.transform.position = TargetLocation;
             }
         }
         #endregion
     
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            if(controller.GetPressUp(triggerButton))
            {
                ReleaseObjectBall(other.gameObject);
            }
            if (controller.GetPress(triggerButton))
            {
                GrabObjectBall(other.gameObject);
            }
        }
    }

    void ReleaseObjectBall(GameObject obj)
    {
        obj.transform.SetParent(null);
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<Rigidbody>().velocity = controller.velocity * 2f;
        obj.GetComponent<Rigidbody>().angularVelocity = controller.angularVelocity;
    }
    void GrabObjectBall(GameObject obj)
    {
        obj.transform.SetParent(gameObject.transform);
        obj.GetComponent<Rigidbody>().isKinematic = true;
        controller.TriggerHapticPulse(2000);
    }

}
