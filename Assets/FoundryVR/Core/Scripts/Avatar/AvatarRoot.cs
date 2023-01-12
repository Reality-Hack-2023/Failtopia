using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

//THIS SCRIPT STAYS ON THE PLAYER PREFAB TO GIVE REFERENCES TO THE TRACKED OBJECTS
public class AvatarRoot : MonoBehaviour
{
    [Header("Referances")]
    [Tooltip("The tracked objects (hmd, right controller, left controller)")]
    public Transform headTracked, rightHandTracked, leftHandTracked;

    [Header("Offsets")] [Tooltip("The offsets of the arms / hands")]
    public Vector3 headOffset, neckoffset, rightHandOffset, leftHandOffset, headOffsetRotation, rightHandOffsetRotation, leftHandOffsetRotation;

    public InputAction gripR, triggerR, thumbR, gripL, triggerL, thumbL;
    [Range(0,90)] public float totalFingerCurve;
    [Range(0,90)] public float minFingerCurve;
    [Range(0,90)] public float minThumbCurve;
    [Range(0,90)] public float totalThumbCurve;

    //Enable all our inputs to make sure they work
    private void Start()
    {
        gripR.Enable();
        triggerR.Enable();
        thumbR.Enable();
        gripL.Enable();
        triggerL.Enable();
        thumbL.Enable();
    }
}
