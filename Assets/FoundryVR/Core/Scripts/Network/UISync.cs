using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.UI;

public class UISync : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider slider;

    private float sliderValue;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(slider.value);
        }
        else
        {
            slider.value = (float)stream.ReceiveNext();
        }
    }

    public void SwapOwnership()
    {
        photonView.RequestOwnership();
    }
}
