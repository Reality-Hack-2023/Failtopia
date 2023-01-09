using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class UIOwnershipFixer : MonoBehaviourPunCallbacks
{
    public PhotonView player;

    public void TakeOwnerShip(HoverEnterEventArgs args)
    {
        args.interactableObject.transform.GetComponent<PhotonView>().TransferOwnership(player.Controller);
    }
}
