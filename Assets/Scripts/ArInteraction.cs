using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion;
using UnityEngine.Animations.Rigging;

public class ArInteraction : MonoBehaviour
{
    //Vector3 originalPos;

    ManoGestureContinuous grab;
    ManoGestureContinuous pinch;
    ManoGestureTrigger click;
    ManoGestureTrigger release;
    ManoGestureContinuous pointer;
    ManoGestureTrigger drop;

    [SerializeField] Animator animator;
    [SerializeField] Animation animation;

    //int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        //originalPos = gameObject.transform.position;

        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        release = ManoGestureTrigger.RELEASE_GESTURE;
        pointer = ManoGestureContinuous.POINTER_GESTURE;
        drop = ManoGestureTrigger.DROP;
        animator = gameObject.GetComponent<Animator>();
        animation = gameObject.GetComponent<Animation>();
        gameObject.GetComponent<RigBuilder>().enabled = false;
        animation.Play("Idle_1");

    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "PrefabChicken")
        {
            gameObject.GetComponent<RigBuilder>().enabled = enabled;
            animation.Play("Idle_1");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(" [OnTriggerStay] >>> [] >>> other.name:  " + other.gameObject.name);
        if (gameObject.name == "ArmatureChicken")
        {   
            //transform.GetComponent<Animator>().enabled = true;
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
            {
                //chickenAnimator.Play("Idle_2", 0, 0.0f);
                transform.parent.GetComponent<Animation>().Play("Idle_2");
                //animation.Play("Idle_2");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                //Do Something
                //chickenAnimator.Play("Eat", 0, 0.0f);

                //gameObject.GetComponentInChildren<RigBuilder>().enable = false;
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.parent.GetComponent<Animation>().Play("Eat");
                //animation.Play("Eat");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
                transform.parent.GetComponent<Animation>().Play("Eat");
                //animation.Play("Walk");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == drop)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.parent.GetComponent<Animation>().Play("Idle_1");
                //animation.Play("Idle_1");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.parent = other.gameObject.transform;
                transform.parent.GetComponent<Animation>().Play("Idle_2");
                //animation.Play("Idle_2");
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.parent = null;
                transform.parent.GetComponent<Animation>().Play("Idle_1");
                //animation.Play("Idle_1");
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else if(gameObject.name != "ArmatureChicken")
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
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == drop)
            {
                //Do something
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
    private void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "ArmatureChicken")
        {
            gameObject.GetComponent<RigBuilder>().enabled = false;
            transform.parent.GetComponent<Animation>().Play("Idle_1");
            //animation.Play("Idle_1");
        }
    }
}
