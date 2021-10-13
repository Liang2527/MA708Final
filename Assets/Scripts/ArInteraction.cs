using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion;

public class ArInteraction : MonoBehaviour
{
    //Vector3 originalPos;

    ManoGestureContinuous grab;
    ManoGestureContinuous pinch;
    ManoGestureTrigger click;
    ManoGestureTrigger release;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        //originalPos = gameObject.transform.position;

        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        release = ManoGestureTrigger.RELEASE_GESTURE;
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {
            //Do Something
            
            //transform.parent = null;
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
        {
            transform.parent = other.gameObject.transform;
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
        {
            transform.parent = null;
        }
    }
}
