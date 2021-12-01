using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArObjectMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    public float rotateSpeed = 0.5f;
    public GameObject chicken;
    public bool chickenWalk = false;

    private float distance;

    
    //private Coroutine LookCoroutine;

    //private Transform tempPosition;
    //private Transform tempRotation;
    //private Transform tempTarget;

    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if(distance > 2f)
        {
            var tempPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            var tempRotation = new Vector3(target.position.x, this.transform.position.y, target.position.z);

            transform.position = Vector3.Lerp(transform.position, tempPosition, speed * Time.deltaTime);
            transform.LookAt(tempRotation);

            if (chickenWalk == false)
            {
                chicken.GetComponent<Animator>().Play("Walk", 0, 0f);
                chickenWalk = true;
            }
        }
        if(distance > 1.95f && distance < 2f)
        {
            chicken.GetComponent<Animator>().Play("Idle_1", 0, 0f);
            chickenWalk = false;
        }
    }

}
