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

    [Tooltip("Minumum acceleration value to keep it non-zero and non-negative.")]
    public float minAccelValue = 5.0f;
    [Tooltip("Minumum speed value to keep it non-zero and non-negative.")]
    public float minSpeedValue = 0.01f;
    [Tooltip("Minumum turn speed value to keep it non-zero and non-negative.")]
    public float minTurnSpeedValue = 2;
    [Tooltip("The value to sum or subtract from the fish's acceleration, speed and turn speed.")]
    public float valueToAffect = 0.5f;

    private bool _updateLimiter = true;
    bool isHidden;
    bool _isclicked;
    bool _grabLimiter;

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
                if(!_isclicked)
                {
                    _isclicked = true;
                    fishController.GetComponent<FishFlockController2>().groupAreaDepth = fishController.GetComponent<FishFlockController2>().speedVariation = 2;
                }
                else if (_isclicked)
                {
                    _isclicked = false;
                    fishController.GetComponent<FishFlockController2>().groupAreaDepth = fishController.GetComponent<FishFlockController2>().speedVariation = 0.6f;
                }

                Debug.Log("@Script: " + this.name.ToString() + " >> clicked");
                _updateLimiter = false;
                StartCoroutine("resetUpdateLimiter", 0.5f);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            {
                if (!_grabLimiter)
                {
                    transform.position = targetObject.transform.position;
                    fishController.GetComponent<FishFlockController2>().groupAreaSpeed = fishController.GetComponent<FishFlockController2>().neighbourDistance = 1.5f;
                
                    Debug.Log("@Script: " + this.name.ToString() + " >> grab");
                    _updateLimiter = false;
                    StartCoroutine("resetUpdateLimiter", 0.5f);
                    _grabLimiter = true;
                }
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
            {
                transform.position = targetObject.transform.position;
                fishController.GetComponent<FishFlockController2>().groupAreaSpeed = fishController.GetComponent<FishFlockController2>().neighbourDistance = 10f;

                Debug.Log("@Script: " + this.name.ToString() + " >> release");
                _updateLimiter = false;
                _grabLimiter = false;
                StartCoroutine("resetUpdateLimiter", 0.5f);
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
            _updateLimiter = true;
        }
    }
}