using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoundryNetwork.Email;

public class EmailTester : MonoBehaviour
{
    public EmailHandler.EmailBody EmailBody;
    
    
    private void Start()
    {
        EmailHandler.SendEmail("Spatial Ape", "woodjoe406@gmail.com", "josephwood0105@gmail.com", JsonUtility.ToJson(EmailBody), "");
    }
}
