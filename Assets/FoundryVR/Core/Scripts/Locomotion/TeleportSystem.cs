using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class TeleportSystem : MonoBehaviour
{
    public TeleportHand hand;

    private void Update()
    {
        if (hand.teleportButton.ReadValue<float>() < 1 && hand.locationShowerInstance != null) 
        {
            transform.position = hand.teleportLocation;
            hand.teleportLocation = transform.position;

            Destroy(hand.locationShowerInstance.gameObject);
            hand.showerEnabled = false;
        }
    }
}
