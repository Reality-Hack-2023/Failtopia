using System;
using UnityEngine;
using Photon.Pun;

public class AvatarSelection : MonoBehaviourPunCallbacks
{
    public GameObject[] defaultAvatars;
    private int selection;

    private void Start()
    {
        selection = PlayerPrefs.GetInt("SelectedAvatar");
        ActivateChosenAvatar();
    }

    public void ActivateChosenAvatar()
    {
        if (photonView.IsMine)
        {
            defaultAvatars[selection].SetActive(true);
            photonView.RPC("ActivateSelectedOnOtherClients", RpcTarget.OthersBuffered, photonView.OwnerActorNr.ToString(), selection);
        }
    }

    [PunRPC]
    public void ActivateSelectedOnOtherClients(string actor, int _selection)
    {
        if (photonView.OwnerActorNr.ToString() != actor)
        {
            return;
        }

        defaultAvatars[_selection].SetActive(true);
    }
}
