using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishFlock;
using UnityEngine.XR.ARFoundation;

public class FishControllerHand : MonoBehaviour
{
    ManoGestureContinuous grab;
    ManoGestureContinuous pinch;
    ManoGestureTrigger click;
    ManoGestureTrigger release;
    ManoGestureContinuous pointer;
    ManoGestureTrigger drop;

    [SerializeField] GameObject camera;
    [SerializeField] GameObject fishController;
    [SerializeField] Transform targetObject;

    private void Start()
    {
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        release = ManoGestureTrigger.RELEASE_GESTURE;
        pointer = ManoGestureContinuous.POINTER_GESTURE;
        drop = ManoGestureTrigger.DROP;
    }

    private void Update()
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {
            transform.position = new Vector3(targetObject.position.x, targetObject.position.y, targetObject.position.z + 30);
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            transform.position = targetObject.transform.position;
    }

    public void Hide()
    {
        if (fishController.activeSelf == true)
        {
            fishController.SetActive(false);
            fishController.GetComponent<FishFlockController2>().OnDestroy();
        }
            
        if (fishController.activeSelf == false)
        {
            fishController.SetActive(true);
            camera.GetComponent<AROcclusionManager>().enabled = false;
        }
    }

}
