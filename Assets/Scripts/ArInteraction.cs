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

    [SerializeField] Transform chickenParent;
    private Transform _chickenParentHolder;

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
        _chickenParentHolder = chickenParent.parent.transform;
        //animation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "PrefabChicken")
        {
            gameObject.GetComponent<RigBuilder>().enabled = false;
            animator.enabled = true;
            animator.Play("Idle_1",0,0f);
            
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
                animator.Play("Idle_2",0,0f);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                animator.Play("Eat",0,0f);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.parent.transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);

                chickenParent.GetComponentInParent<Animation>().Play("Walk 1");
                animator.Play("Walk", 0, 0f);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == drop)
            {
                animator.Play("Idle_1",0,0f);
                gameObject.GetComponent<RigBuilder>().enabled = true;
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            {
                gameObject.GetComponent<RigBuilder>().enabled = false;
                transform.parent.parent = other.gameObject.transform;
                animator.Play("Idle_2",0,0f);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
            {
                Vector3 temp = transform.position;
                transform.parent.parent = _chickenParentHolder;
                transform.position = temp;
                animator.Play("Idle_1",0,0f);
                gameObject.GetComponent<RigBuilder>().enabled = true;
            }
        }

        // below for all other objects 

        else if (gameObject.name != "ArmatureChicken")
        {
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
            {
                //Do Something
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                //Do Something
                //ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.poi.

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
            gameObject.GetComponent<RigBuilder>().enabled = true;
            animator.Play("Idle_1");
        }
    }
}
