using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TrackedL : MonoBehaviour
{
    public float armLength = 3f;
    public float heightAdjust = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 lPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
        Vector3 hPos = InputTracking.GetLocalPosition(XRNode.Head);
        Quaternion lRot = InputTracking.GetLocalRotation(XRNode.LeftHand);

        var direction = new Vector3(lPos.x - hPos.x, lPos.y, lPos.z - hPos.z);
        transform.localPosition = direction  * armLength - Vector3.up * heightAdjust;
        transform.localRotation = lRot;
    }
}
