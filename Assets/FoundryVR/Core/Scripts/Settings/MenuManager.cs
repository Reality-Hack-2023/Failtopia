using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class MenuManager : MonoBehaviour
{
    public List<string> pressedKeys = new List<string>();
    public TMP_Text usernameText;

    private string removedCharacter;
    public void RegisterUserOnConnect()
    {
        PlayerPrefs.SetString("username", usernameText.text);
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void SaveAvatarSelection(int number)
    {
        PlayerPrefs.SetInt("SelectedAvatar", number);
        Debug.Log($"Saved number : {number}");
    }
    
    public void RegisterKeystroke (string key)
    {
        Debug.Log(key);
        
        string pressed = key;
        pressedKeys.Add(pressed);

        usernameText.text = ListToText(pressedKeys);
    }

    string ListToText(List<string> stringList)
    {
        string result = "";
        
        foreach (string letter in stringList)
        {
            result += letter;
        }

        return result;
    }

    public void ClearUsername()
    {
        pressedKeys.Clear();
        usernameText.text = "";
    }
}
