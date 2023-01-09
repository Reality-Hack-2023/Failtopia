using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using ReadyPlayerMe;

//THIS SCRIPT CONTROLLS AVATAR LOADING IF YOU MAKE A CUSTOM SCRIPT TO HANDLE THIS FROM A WEB API ECT USE THE PUBLIC FUNCTION WITH THE URL PASSED INTO, IT WILL HANDLE NETWORKING

public class Avatar : MonoBehaviourPunCallbacks
{
    private string avatarURL;

    public GameObject defaultAvatar;

    private void OnGUI()
    {
        avatarURL = GUILayout.TextField(avatarURL);

        if (GUILayout.Button("Change Avatar")) 
        {
            if (this.photonView.IsMine)
            {
                LoadAvatar(avatarURL);
            }
        }
    }

    void LoadAvatar(string avatarURL) 
    {
        Destroy(defaultAvatar);
        
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(avatarURL);
        avatarLoader.OnCompleted += AvatarLoader_OnCompleted;
        avatarLoader.OnFailed += AvatarLoader_OnFailed;

        this.photonView.RPC("LoadAvatarOthers", RpcTarget.OthersBuffered, photonView.OwnerActorNr.ToString(), avatarURL);
    }

    [PunRPC]
    void LoadAvatarOthers(string actor, string url) 
    {
        if (photonView.OwnerActorNr.ToString() != actor) 
        {
            return;
        }

        Destroy(defaultAvatar);

        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(url);
        avatarLoader.OnCompleted += AvatarLoader_OnCompleted;
        avatarLoader.OnFailed += AvatarLoader_OnFailed;

        Debug.Log($"actor : {actor} wants to load avatar {url}");
    }

    private void AvatarLoader_OnFailed(object sender, FailureEventArgs e)
    {
        Debug.Log($"avatar load failed");
    }

    private void AvatarLoader_OnCompleted(object sender, CompletionEventArgs e)
    {
        Debug.Log($"avatar loaded");
        e.Avatar.transform.parent = this.transform;
        e.Avatar.AddComponent<AvatarSolver>();
    }
}
