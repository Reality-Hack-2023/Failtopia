using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class TeleportHand : MonoBehaviour
{
    public InputAction teleportButton;
    
    public float maxTeleportDistance;
    public Transform raycastOffset;

    public GameObject teleportPoint;

    private LineRenderer LR;

    public Vector3 teleportLocation;

    public LayerMask teleportMask;

    [HideInInspector] public bool showerEnabled;

    [HideInInspector]public Transform locationShowerInstance;

    private void Start()
    {
        teleportButton.Enable();
    }

    private void Update()
    {
        Ray ray = new Ray(raycastOffset.position, raycastOffset.forward);


        if (teleportButton.ReadValue<float>() > 0)
        {
            if (Physics.Raycast(ray, out RaycastHit teleportInfo, maxTeleportDistance, teleportMask))
            {
                if (teleportInfo.transform.GetComponent<TeleportArea>())
                {
                    //can teleport
                    teleportLocation = teleportInfo.point;

                    if (!showerEnabled)
                    {
                        locationShowerInstance = Instantiate(teleportPoint, teleportLocation, Quaternion.identity).transform;
                        showerEnabled = true;
                    }

                    if (locationShowerInstance != null)
                    {
                        locationShowerInstance.position = teleportLocation;
                    }
                }
                else if (teleportInfo.transform == null || teleportInfo.transform.GetComponent<TeleportArea>() == null && locationShowerInstance != null)
                {
                    Destroy(locationShowerInstance.gameObject);
                    showerEnabled = false;
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction);
    }
}
