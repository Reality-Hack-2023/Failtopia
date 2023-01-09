using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using FoundryNetwork.Interaction;
using Photon.Realtime;

public class Interactable : MonoBehaviourPunCallbacks
{
    public GrabOptions grabOptions;

    [Tooltip("velocityTrack")] public float velocityMultiplier;

    public Animator animator;
    public string animationParameter;
}

[System.Serializable]
public partial class GrabOptions
{
    public InteractionVisualType interaction;
    public InteractableType interactableType;
    public GrabType grabType;
}
