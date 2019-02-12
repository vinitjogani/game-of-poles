using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TrackedL : MonoBehaviour
{
    public Transform offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = offset.localPosition + InputTracking.GetLocalPosition(XRNode.LeftHand);
        transform.localRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
    }
}
