using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;

public class StickyNote : MonoBehaviourPunCallbacks, IPunObservable
{
    public string text;
    public TMP_Text textOutput;

    public bool editing;

    private void Update()
    {
        if (textOutput.text != text)
        {
            textOutput.text = text;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(textOutput.text);
        }
        else
        {
            textOutput.text = (string)stream.ReceiveNext();
        }
    }
}
