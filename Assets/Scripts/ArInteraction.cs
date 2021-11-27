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
    ManoGestureContinuous pointer;

    [SerializeField] Animator animator;
    [SerializeField] Animation animation;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        //originalPos = gameObject.transform.position;

        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        release = ManoGestureTrigger.RELEASE_GESTURE;
        pointer = ManoGestureContinuous.POINTER_GESTURE;

        animator = gameObject.GetComponent<Animator>();
        animation = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(" [OnTriggerStay] >>> [] >>> other.name:  " + other.gameObject.name);
        if (gameObject.name == "PrefabChicken")
        {   
            Debug.Log("[ARinterection] >>> OnTriggerStay >>> PrefabChicken >>> ? ");
            //transform.GetComponent<Animator>().enabled = true;
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
            {
                //chickenAnimator.Play("Idle_2", 0, 0.0f);
                animation.Play("Idle_2");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                //Do Something
                //chickenAnimator.Play("Eat", 0, 0.0f);
                animation.Play("Eat");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
                animation.Play("Walk");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            {
                transform.parent = other.gameObject.transform;
                animation.Play("Idle_2");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
            {
                transform.parent = null;
                animation.Play("Idle_1");
            }
        }

        else if(gameObject.name != "PrefabChicken")
        {
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
            {
                //Do Something
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                //Do Something
                //ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.poi.
                GetComponent<Animation>().Play();
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
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
        else
            Debug.Log(" [ARinterecation] >>> [onCollisionStay] >>> Nothiing to do");

    }
}
