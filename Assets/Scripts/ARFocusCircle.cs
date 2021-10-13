using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(ARRaycastManager))]
public class ARFocusCircle : MonoBehaviour
{
    //public GameObject scanText;
    //public GameObject placeText;

    int objIndex;

    public GameObject[] virtual_objects;
    public GameObject[] buttons;
    public Camera arCam;
    public GameObject placementIndicator;
    public GameObject planetObject;

    private ARSessionOrigin arOrigin;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool planetObjectToggle = false;

    private bool placementIndicatorEnabled = true;

    bool isUIHidden = true;

    void Start()
    {
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (isUIHidden == false)
        {
            if (placementIndicatorEnabled == true)
            {
                UpdatePlacementPose();
                UpdatePlacementIndicator();
            }
        }


        //if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    PlaceObject();
        //}
    }

    public void HideUI() {

        if (isUIHidden == false)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            placementIndicatorEnabled = false;
            placementIndicator.SetActive(false);

            isUIHidden = true;
        }

        else if (isUIHidden == true) {

            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }

            placementIndicatorEnabled = true;
            placementIndicator.SetActive(true);

            isUIHidden = false;

        }
    }

    public void PlaceObject() {

        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < buttons.Length; i++) {

            if (buttons[i].name == buttonName) {

                objIndex = i;
            }
        }

        virtual_objects[objIndex].SetActive(true);
        virtual_objects[objIndex].transform.position = placementPose.position;
        virtual_objects[objIndex].transform.rotation = placementPose.rotation;
    }

    public void showPlanet()
    {
        if (!planetObject.activeSelf)
            planetObject.SetActive(true);
            
        else if (planetObject.activeSelf)
            planetObject.SetActive(false);
    }

    private void UpdatePlacementIndicator()
    {

        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);

            foreach (var button in buttons) {
                button.gameObject.SetActive(true);
            }
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);

            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated);
        //raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated);
        
        //PlaneEstimated
        //Debug.Log(" >>> Position of the Camera is: " + placementPose.position.ToString() + " <<< ");
        //Debug.Log(" >>> Position of the Camera is: " + placementPose.rotation.ToString() + " <<< ");
        //Debug.Log(" >>>>>>>>>> List of Raycast Hits are: " + hits.ToString() + " <<< ");

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;


            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

            foreach (var plane in arOrigin.GetComponent<ARPlaneManager>().trackables)
            {
                if (isUIHidden == true)
                    plane.gameObject.SetActive(true);
                if (isUIHidden == false)
                    plane.gameObject.SetActive(false);
            }
            //Debug.Log(" >>> List of Raycast Hits are: " + hits.ToString() + " <<< ");
            //Debug.Log(" >>> Hitted!!! Position of the Hit is: " + placementPose.position.ToString() + " <<< ");
            //Debug.Log(" >>> Hitted!!! Position of the Hit is: " + placementPose.rotation.ToString() + " <<< ");
        }
    }
}
