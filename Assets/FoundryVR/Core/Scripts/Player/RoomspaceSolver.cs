using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomspaceSolver : MonoBehaviour
{
    public CharacterController CC;
    public Transform head;

    public Transform xrOrigin;

    public GameObject usernameText;
    public float usernameTextOffset = 0.15F;

    private Vector3 prevPos;

    private void Update()
    {
        CC.height = Vector3.Distance(head.position, transform.position);
        CC.center = new Vector3(0, CC.height * .5F, 0);

        usernameText.transform.position = new Vector3(usernameText.transform.position.x, CC.height + usernameTextOffset, usernameText.transform.position.z);

        Vector3 irlMovement = xrOrigin.InverseTransformPoint(head.position) - prevPos;

        irlMovement.y = 0;

        irlMovement = xrOrigin.TransformDirection(irlMovement);
        transform.position = (transform.position + irlMovement);
        //transform.position += irlMovement;

        prevPos = xrOrigin.InverseTransformPoint(head.position);
        xrOrigin.localPosition = -xrOrigin.InverseTransformPoint(head.position);

        xrOrigin.localPosition = new Vector3(xrOrigin.localPosition.x, 0, xrOrigin.localPosition.z);
    }
}
