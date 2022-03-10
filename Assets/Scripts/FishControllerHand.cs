using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishFlock;
using UnityEngine.XR.ARFoundation;

public class FishControllerHand : MonoBehaviour
{
    ManoGestureContinuous grab;
    //ManoGestureContinuous pinch;
    ManoGestureTrigger click;
    ManoGestureTrigger release;
    //ManoGestureContinuous pointer;
    //ManoGestureTrigger drop;

    [SerializeField] GameObject camera;
    [SerializeField] GameObject fishController;
    [SerializeField] Transform targetObject;
    [SerializeField] GameObject ARPlaneSetupManager;

    private bool _updateLimiter;
    bool isHidden;

    private void Start()
    {
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        //pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        release = ManoGestureTrigger.RELEASE_GESTURE;
        //pointer = ManoGestureContinuous.POINTER_GESTURE;
        //drop = ManoGestureTrigger.DROP;
    }

    private void Update()
    {
        if (_updateLimiter)
        {
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                transform.position = camera.transform.position + camera.transform.forward * 20 + camera.transform.up * 20;
                fishController.GetComponent<FishFlockController2>().groupAreaSpeed = fishController.GetComponent<FishFlockController2>().groupAreaSpeed * 2;
                _updateLimiter = false;
                StartCoroutine("resetUpdateLimiter", 1.0);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            {
                transform.position = targetObject.transform.position;
                fishController.GetComponent<FishFlockController2>().groupAreaDepth = fishController.GetComponent<FishFlockController2>().groupAreaDepth / 2;
                fishController.GetComponent<FishFlockController2>().groupAreaHeight = fishController.GetComponent<FishFlockController2>().groupAreaHeight / 2;
                fishController.GetComponent<FishFlockController2>().groupAreaWidth = fishController.GetComponent<FishFlockController2>().groupAreaWidth / 2;
                fishController.GetComponent<FishFlockController2>().groupAreaSpeed = fishController.GetComponent<FishFlockController2>().groupAreaSpeed * 2;
                _updateLimiter = false;
                StartCoroutine("resetUpdateLimiter", 1.0);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
            {
                transform.position = targetObject.transform.position;
                fishController.GetComponent<FishFlockController2>().groupAreaDepth = fishController.GetComponent<FishFlockController2>().groupAreaDepth * 2;
                fishController.GetComponent<FishFlockController2>().groupAreaHeight = fishController.GetComponent<FishFlockController2>().groupAreaHeight * 2;
                fishController.GetComponent<FishFlockController2>().groupAreaWidth = fishController.GetComponent<FishFlockController2>().groupAreaWidth * 2;
                fishController.GetComponent<FishFlockController2>().groupAreaSpeed = fishController.GetComponent<FishFlockController2>().groupAreaSpeed / 2;
                _updateLimiter = false;
                StartCoroutine("resetUpdateLimiter", 1.0);
            }
        }
    }

    private IEnumerator resetUpdateLimiter(float delay)
    {
        yield return new WaitForSeconds(delay);
        _updateLimiter = true;
    }

    public void Hide()
    {
        if (fishController.activeSelf)
        {
            fishController.GetComponent<FishFlockController2>().fishesCount = 0;
            StartCoroutine("resetFishesCount", 0.1f);
            Debug.Log("isHiddent");
        }    
        else if (!fishController.activeSelf)
        {
            fishController.GetComponent<FishFlockController2>().fishesCount = 100;
            StartCoroutine("resetFishesCount", 0.1f);
            camera.GetComponent<AROcclusionManager>().enabled = false;
            Debug.Log("isNOTHiddent");
        }
    }

    private IEnumerator resetFishesCount(float delay)
    {
        if (fishController.activeSelf)
        {
            yield return new WaitForSeconds(delay);
            fishController.SetActive(false);
        }
        else if (!fishController.activeSelf)
        {
            yield return new WaitForSeconds(delay);
            ARPlaneSetupManager.GetComponent<PlaneSetupManager>().SetOcclusionMaterial();
            fishController.SetActive(true);
        }
    }
}