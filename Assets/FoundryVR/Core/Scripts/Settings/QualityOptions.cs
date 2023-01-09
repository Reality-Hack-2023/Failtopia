using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class QualityOptions : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown turnStyleDropdown;

    public string[] qualityOptions;

    private void Start()
    {
        qualityOptions = QualitySettings.names;
    }

    public void Confirm() 
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void ConfirmLocomotion()
    {
        //0 = snap
        //1 = smooth
        PlayerPrefs.SetInt("turnoption", turnStyleDropdown.value);
    }
}
