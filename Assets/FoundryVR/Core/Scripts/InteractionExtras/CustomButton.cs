using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class CustomButton : MonoBehaviour
{
    public Color highlightedColor;
    public Color startColor;

    public bool emmisionChange;
    public MeshRenderer emmisionOutline;
    
    MeshRenderer render;

    private void Start()
    {
        if (emmisionChange)
            emmisionOutline = GetComponent<MeshRenderer>();
    }

    public void HoverEnter(HoverEnterEventArgs args)
    {
        render = args.interactableObject.transform.GetComponent<MeshRenderer>();
        
        if (!emmisionChange)
        {
            render.material.color = highlightedColor;
        }
        else
        {
            emmisionOutline.enabled = true;
        }
    }
    
    public void HoverExit(HoverExitEventArgs args)
    {
        render = args.interactableObject.transform.GetComponent<MeshRenderer>();
        
        if (!emmisionChange)
        {
            render.material.color = startColor;
        }
        else
        {
            emmisionOutline.enabled = false;
        }
    }
}
