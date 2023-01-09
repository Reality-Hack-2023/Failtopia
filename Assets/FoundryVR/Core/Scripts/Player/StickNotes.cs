using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class StickNotes : MonoBehaviour
{
    public PlayerKeyboard keyboard;
    public GameObject stickyNote;

    public string stickNoteText = "Testing";

    public Transform stickNoteSP;

    private StickyNote SN;
    
    public void SpawnStickNote()
    {
        if (!keyboard.gameObject.activeSelf)
        {
            keyboard.gameObject.SetActive(true);
        }

        GameObject stickyNoteInstance = PhotonNetwork.Instantiate(stickyNote.name, stickNoteSP.position, stickNoteSP.rotation);
        SN = stickyNoteInstance.GetComponent<StickyNote>();
        SN.text = "";
        SN.editing = true;
    }

    public void Confirm()
    {
        SN.editing = false;
        SN = null;
        keyboard.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (SN != null && SN.editing)
        {
            SN.text = keyboard.output;
        }
    }
}
