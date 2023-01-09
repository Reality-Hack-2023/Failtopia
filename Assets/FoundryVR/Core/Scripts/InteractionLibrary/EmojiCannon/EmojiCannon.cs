using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Photon.Pun;

namespace Foundry
{
    public class EmojiCannon : MonoBehaviourPunCallbacks
    {
        public GameObject[] emojis;

        public Transform barrel;

        private int index;

        public void ChangeIndex(int indexTarget)
        {
            index = indexTarget;
        }

        public void ShootEmoji()
        {
            PhotonNetwork.Instantiate(Path.Combine("Emojis", emojis[index].name), barrel.position, barrel.rotation);
        }
    }
}
