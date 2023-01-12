using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinPad : MonoBehaviour
{
    public string Password = "1111";
    public string KeysPressed;
    public SceneChangePortal SceneChangePortalScript;
    [SerializeField] private TextMeshProUGUI _pinPadText;


    public void AddKeyPress(string KeyPress)
    {
        KeysPressed += KeyPress;
        _pinPadText.text = KeysPressed.ToString();
        Debug.Log(KeyPress);
    }

    public void ClearKeysOut()
    {
        KeysPressed = "";
        _pinPadText.text = "Enter Pin";
        Debug.Log("Cleared");
    }

    public void EnterKey()
    {
        Debug.Log("Enter key pressed");
        if (KeysPressed == Password)
        {
            Debug.Log("Password correct");
            //activate portal
            _pinPadText.text = "Access Granted";
            SceneChangePortalScript.ActivatePortal();
        }
    }
}
