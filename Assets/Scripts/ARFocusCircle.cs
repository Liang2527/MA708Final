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

    [SerializeField] GameObject FishController;

    int objIndex;

    public GameObject[] virtual_objects;
    public GameObject[] buttons;
    public GameObject arCam;
    public GameObject placementIndicator;
    public GameObject planetObject;
    public GameObject hideUIButton;

    private ARSessionOrigin arOrigin;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool planetObjectToggle = false;

    private bool placementIndicatorEnabled = true;

    bool isUIHidden = true;

    void Start()
    {
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
    }

    public void displayHideUIButton()
    {
        if (hideUIButton.activeSelf == false)
            hideUIButton.SetActive(true);
        else
            hideUIButton.SetActive(false);
    }

    public void HideFish()
    {
        if (FishController.activeSelf == true)
            FishController.SetActive(false);
        if (FishController.activeSelf == false)
        {
            FishController.SetActive(true);
            HideUI();
        }
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

            arCam.GetComponent<AROcclusionManager>().enabled = true;
            placementIndicatorEnabled = true;
            placementIndicator.SetActive(true);
            
            isUIHidden = false;

        }
    }

    public void hideAllObjects()
    {
        for (int i = 0; i < virtual_objects.Length; i++)
        {
            virtual_objects[i].SetActive(false);
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
        {
            planetObject.SetActive(true);
            planetObject.transform.position = arCam.transform.position + arCam.transform.forward * 1;
        }
            
            
            
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


        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;


            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }
    }
}
