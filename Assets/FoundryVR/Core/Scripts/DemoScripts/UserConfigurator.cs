using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using ReadyPlayerMe;
using TMPro;

public class UserConfigurator : MonoBehaviourPunCallbacks
{
    public Behaviour[] localScripts;
    public string username;
    public TMP_Text usernameText;

    public GameObject[] objectsToDisable;

    private void Start()
    {
        username = PlayerPrefs.GetString("username");

        //Set username locally
        if (photonView.IsMine)
        {
            usernameText.text = username;
            photonView.RPC("ConfigureClientUsername", RpcTarget.OthersBuffered, username, photonView.OwnerActorNr.ToString());
        }
        
        if (!this.photonView.IsMine)
        {
            foreach (Behaviour localScript in localScripts)
            {
                localScript.enabled = false;
            }

            foreach (GameObject objectToDisable in objectsToDisable)
            {
                objectToDisable.SetActive(false);
            }
        }
    }

    [PunRPC]
    void ConfigureClientUsername(string _username, string actor)
    {
        if (photonView.OwnerActorNr.ToString() != actor)
        {
            return;
        }

        usernameText.text = _username;
    }
}
