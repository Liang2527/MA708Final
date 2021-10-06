using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArObjectInteraction : MonoBehaviour
{
    ManoGestureContinuous grab;
    ManoGestureContinuous pinch;
    ManoGestureTrigger click;

    // Start is called before the first frame update
    void Start()
    {
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
        {
            transform.parent = other.gameObject.transform;
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {

        }
    }
}
