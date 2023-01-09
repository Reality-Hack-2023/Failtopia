using System.Collections;
using System.Collections.Generic;
using FoundryNetwork.Interaction;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Interactable itemNearHand;
    private Rigidbody itemInHand;

    [Header("Input")]
    public InputAction gripBind;
    public InputAction triggerBind;

    [Header("Player")] public Player localPlayer;

    [Header("Grab Options")]
    public Transform palmOffset;

    Vector3 lastPosition;

    public Vector3 velocity;

    private void Start()
    {
        gripBind.Enable();
        triggerBind.Enable();

        localPlayer = transform.root.GetComponent<PhotonView>().Owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>()) 
        {
            itemNearHand = other.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            itemNearHand = null;
        }
    }

    private void Update()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;

        //Grab Initiated
        if (gripBind.ReadValue<float>() > 0)
        {
            DetectGrab();
        }
        else if (gripBind.ReadValue<float>() < 0.5F)
        {
            Drop();
        }
        
        lastPosition = transform.position;
    }

    void DetectGrab()
    {
        itemNearHand.photonView.RequestOwnership();
            
        if (itemNearHand.grabOptions.grabType == GrabType.Kinematic)
        {
            if (itemNearHand.GetComponent<Rigidbody>())
            {
                Rigidbody itemNearHandRB = itemNearHand.GetComponent<Rigidbody>();
                itemNearHandRB.isKinematic = true;
                    
                itemNearHand.transform.parent = transform;
                itemNearHand.transform.localPosition = palmOffset.localPosition;
            }
            else
            {
                itemNearHand.transform.parent = transform;
                itemNearHand.transform.localPosition = palmOffset.localPosition;
            }
        }
        else if(itemNearHand.grabOptions.grabType == GrabType.VelocityTrack && itemNearHand.GetComponent<Rigidbody>())
        {
            itemInHand = itemNearHand.GetComponent<Rigidbody>();

            Vector3 direction = itemInHand.transform.position - transform.position;
            itemInHand.AddForce((direction * direction.magnitude) * itemNearHand.velocityMultiplier);
        }
    }

    void Drop()
    {
        itemInHand = null;
        itemNearHand = null;
    }
}
