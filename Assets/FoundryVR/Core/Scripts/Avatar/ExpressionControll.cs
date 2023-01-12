using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Voice.Unity;
using UnityEngine;

public class ExpressionControll : MonoBehaviour
{
    [Header("Microphone")] 
    [Range(64,128)] public int sampleWindow = 64;
    public GameObject recorderOrigin;
    public float sensitivty;
    public float threshold;

    [Header("FACE")] public SkinnedMeshRenderer face;

    [Header("OUT")] public float loudness;
    
    private AudioOutCapture recorder;

    private void Update()
    {
        if (recorder == null)
        {
            recorder = gameObject.GetComponentInChildren<AudioOutCapture>();
        }

        if (recorder != null)
        {
            recorder.OnAudioFrame += RecorderOnOnAudioFrame;
        }


        if (face != null)
        {
            face.SetBlendShapeWeight(51,
                Mathf.Lerp(face.GetBlendShapeWeight(51), Mathf.Clamp(loudness, 0, 100), 6 * Time.deltaTime));
        }

        if (loudness < threshold)
        {
            loudness = 0;
        }
    }

    private void RecorderOnOnAudioFrame(float[] arg1, int arg2)
    {
        loudness = arg1[1] + arg1[0] * sensitivty;
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;
        
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
