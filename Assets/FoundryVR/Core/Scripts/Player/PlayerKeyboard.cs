using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboard : MonoBehaviour
{
    public string output;

    private List<string> keys = new List<string>();

    public void KeyStroke(string key)
    {
        string keyPressed = key;
        keys.Add(keyPressed);
    }

    public void Backspace()
    {
        keys.RemoveAt(keys.Count - 1);
    }

    private void Update()
    {
        if(keys.Count != 0)
            output = ListToText(keys);
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
}
