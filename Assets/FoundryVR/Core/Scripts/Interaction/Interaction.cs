using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoundryNetwork.Interaction 
{
    [System.Serializable]
    public enum InteractionVisualType 
    {
        HandPose,
        HideHands
    }

    [System.Serializable]
    public enum InteractableType 
    {
        Grab,
        Button,
        Lever
    }

    [System.Serializable]
    public enum GrabType
    {
        Kinematic,
        VelocityTrack
    }
}
