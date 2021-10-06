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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
