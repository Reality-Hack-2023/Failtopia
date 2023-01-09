using UnityEngine;

using Photon.Pun;

using ReadyPlayerMe;

public class DemoUser : MonoBehaviourPunCallbacks
{
    public Behaviour[] localScripts;
    public GameObject[] objectsLocal;

    private void Start()
    {
        if (!this.photonView.IsMine)
        {
            foreach (Behaviour localScript in localScripts)
            {
                localScript.enabled = false;
            }

            foreach (GameObject local in objectsLocal)
            {
                local.SetActive(false);
            }
        }
    }

}
