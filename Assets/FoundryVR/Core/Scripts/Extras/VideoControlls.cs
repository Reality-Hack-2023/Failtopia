using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControlls : MonoBehaviour
{
    private bool playing;

    private VideoPlayer player;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    public void PlayPauseControl ()
    {
        if (player.isPlaying)
        {
            player.Pause();
        }
        else
        {
            player.Play();
        }
    }
}
