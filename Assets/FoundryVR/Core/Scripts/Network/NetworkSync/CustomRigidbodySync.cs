using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

//Custom RB sync to simulate better physics grabs

namespace Foundry
{
    public class CustomRigidbodySync : MonoBehaviourPunCallbacks, IPunObservable
    {
        private Rigidbody RB;

        private void Start()
        {
            RB = GetComponent<Rigidbody>();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(RB.position);
                stream.SendNext(RB.rotation);
                stream.SendNext(RB.isKinematic);
                stream.SendNext(RB.useGravity);
            }
            else
            {
                RB.position = (Vector3)stream.ReceiveNext();
                RB.rotation = (Quaternion)stream.ReceiveNext();
                RB.isKinematic = (bool)stream.ReceiveNext();
                RB.useGravity = (bool)stream.ReceiveNext();
            }
        }
    }
}
