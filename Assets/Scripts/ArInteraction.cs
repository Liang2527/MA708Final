using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion;
using UnityEngine.Animations.Rigging;

public class ArInteraction : MonoBehaviour
{
    ManoGestureContinuous grab;
    ManoGestureContinuous pinch;
    ManoGestureTrigger click;
    ManoGestureTrigger release;
    ManoGestureContinuous pointer;
    ManoGestureTrigger drop;

    public float speed = 0.5f;

    [SerializeField] Animator animator;
    [SerializeField] Transform foodHolder;
    [SerializeField] Transform chickenParent;
    [SerializeField] Transform pigParent;


    private Transform _chickenParentHolder;
    private Transform _pigParentHolder;
    private bool _isRotate;
    private List<Transform> collisionObjects;
    private Quaternion _tempObjRotation;
    private float _rotationTime;

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

        _chickenParentHolder = chickenParent.parent.transform;
        _pigParentHolder = pigParent.parent.transform;
    }

    private void Update()
    {
        if(_isRotate == true && collisionObjects.Count != 0)
        {
            collisionObjects[0].transform.rotation = Quaternion.Lerp(collisionObjects[0].transform.rotation, _tempObjRotation, speed * Time.deltaTime);
            _rotationTime += speed * Time.deltaTime;
            if (_rotationTime > 1)
                _isRotate = false;
            Debug.Log("@Script: >>" + this.name.ToString() + " @function: [ Update ] >> @Detail: [ Updating... ]");
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        animator = other.gameObject.GetComponent<Animator>();

        if (other.name == "PrefabChicken")
        {
            other.gameObject.GetComponent<RigBuilder>().enabled = false;
            animator.enabled = true;
            animator.Play("Idle_1",0,0f);
        }
        if (other.name == "Pig")
        {
            other.gameObject.GetComponent<RigBuilder>().enabled = false;
            animator.enabled = true;
            animator.Play("Idle", 0, 0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(" [OnTriggerStay] >>> [] >>> other.name:  " + other.gameObject.name);
        if (other.name == "ArmatureChicken" || other.name == "Pig")
        {
            animator = other.GetComponent<Animator>();
            if (other.name == "ArmatureChicken")
            {   
                //transform.GetComponent<Animator>().enabled = true;
                if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
                {
                    animator.Play("Idle_2",0,0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
                {
                    other.gameObject.GetComponent<RigBuilder>().enabled = false;
                    animator.Play("Eat",0,0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
                {
                    other.gameObject.GetComponent<RigBuilder>().enabled = false;
                    other.transform.parent.transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);

                    chickenParent.GetComponentInParent<Animation>().Play("Walk 1");
                    animator.Play("Walk", 0, 0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == drop)
                {
                    animator.Play("Idle_1",0,0f);
                    other.gameObject.GetComponent<RigBuilder>().enabled = true;
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
                {
                    other.gameObject.GetComponent<RigBuilder>().enabled = false;
                    other.transform.parent.parent = gameObject.transform;
                    animator.Play("Idle_2",0,0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
                {
                    Vector3 temp = other.gameObject.transform.position;
                    other.transform.parent.parent = _chickenParentHolder;
                    other.gameObject.transform.position = temp;
                    animator.Play("Idle_1",0,0f);
                    other.gameObject.GetComponent<RigBuilder>().enabled = true;
                }
            }

            if (other.name == "Pig")
            {
                //transform.GetComponent<Animator>().enabled = true;
                if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
                {
                    animator.Play("Idle", 0, 0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
                {
                    other.gameObject.GetComponent<RigBuilder>().enabled = false;
                    animator.Play("Eat_Drink", 0, 0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
                {
                    other.gameObject.GetComponent<RigBuilder>().enabled = false;
                    other.transform.parent.transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);

                    chickenParent.GetComponentInParent<Animation>().Play("Walk 1");
                    animator.Play("Walk", 0, 0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == drop)
                {
                    animator.Play("Rest", 0, 0f);
                    other.gameObject.GetComponent<RigBuilder>().enabled = true;
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
                {
                    other.gameObject.GetComponent<RigBuilder>().enabled = false;
                    other.transform.parent.parent = gameObject.transform;
                    animator.Play("Idle", 0, 0f);
                }
                else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
                {
                    Vector3 temp = other.gameObject.transform.position;
                    other.transform.parent.parent = _pigParentHolder;
                    other.gameObject.transform.position = temp;
                    animator.Play("Idle", 0, 0f);
                    other.gameObject.GetComponent<RigBuilder>().enabled = true;
                }
            }        
        }

        // below for all other objects
        else if (other.transform.parent.name == "FoodItems" || other.transform.parent.name == "HandCollider") 
        {
            Debug.Log(" [OnTriggerStay] >> [] >> other.name:  " + other.gameObject.name);
            
            //animator = other.GetComponentInChildren<Animator>();
            var tempObjPosition = other.gameObject.transform.position;
            
            if(collisionObjects.Count == 0)
            {
                _tempObjRotation = other.gameObject.transform.rotation;
                collisionObjects.Add(other.gameObject.transform);
            }

            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pointer)
            {
                //Do Something
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
            {
                //Do Something
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
            {
                other.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == drop)
            {
                _isRotate = true;
                StartCoroutine("resetList");
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
            {
                other.gameObject.transform.parent = gameObject.transform;
            }
            else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == release)
            {
                Vector3 temp = other.gameObject.transform.position;
                other.gameObject.transform.parent = foodHolder;
                other.gameObject.transform.position = temp;
                Debug.Log("parent set to null " + other.gameObject.transform.parent.name.ToString());
            }
        }
    }

    private IEnumerator resetList()
    {
        yield return new WaitForSeconds(1.0f);
        collisionObjects = new List<Transform>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.name == "ArmatureChicken" || other.transform.parent.name == "Pig")
        {
            animator = other.GetComponent<Animator>();
            other.gameObject.GetComponent<RigBuilder>().enabled = true;
            if(other.gameObject.name == "ArmatureChicken")
                animator.Play("Idle_1");
            else if(other.gameObject.name == "Pig")
                animator.Play("Idle");
        }
        if(other.transform.parent.name == "FoodItems")
        {
            collisionObjects = new List<Transform>();
        }
    }

    public void resetArInterection()
    {
        collisionObjects = new List<Transform>();
        _isRotate = false;
        _rotationTime = 0;
    }
}
