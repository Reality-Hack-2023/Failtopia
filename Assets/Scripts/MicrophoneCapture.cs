using UnityEngine;
using System.Collections;    
    
[RequireComponent(typeof(AudioSource))]
public class MicrophoneCapture : MonoBehaviour
{

    // Material that the tincan will use once it has a recording in it
    public Material materialRecorded;
    // Boolean flags shows if the microphone is connected   
    private bool micConnected = false;
    public bool hasRecorded = false;
    private bool isRecording = false;

    //The maximum and minimum available recording frequencies    
    private int minFreq;
    private int maxFreq;
    private float startRecordingTime;

    //A handle to the attached AudioSource    
    private AudioSource goAudioSource;

    void Start()
    {
        if (hasRecorded)
        {
            this.GetComponent<MeshRenderer>().material = materialRecorded;
        }
        //Check if there is at least one microphone connected    
        if (Microphone.devices.Length <= 0)
        {
            //Throw a warning message at the console if there isn't    
            Debug.LogWarning("Microphone not connected!");
        }
        else //At least one microphone is present    
        {
            //Set our flag 'micConnected' to true    
            micConnected = true;

            //Get the default microphone recording capabilities    
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...    
            if (minFreq == 0 && maxFreq == 0)
            {
                //...meaning 44100 Hz can be used as the recording sampling rate    
                maxFreq = 44100;
            }

            //Get the attached AudioSource component    
            goAudioSource = this.GetComponent<AudioSource>();
        }
    }

    public void OnPickUp(){
        if (hasRecorded){
            PlayRecording();
        }
        else{
            Record();
        }
    }

    void PlayRecording(){
        //play previous recording
        goAudioSource.Play();
    }

    public void Record(){
        //If there is a microphone    
        if (micConnected)
        {
            //If the audio from any microphone isn't being captured    
            if (!Microphone.IsRecording(null))
            {
                //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource  
                startRecordingTime = Time.time;  
                isRecording = true;
                goAudioSource.clip = Microphone.Start(null, true, 20, maxFreq);
            }
            else // No microphone    
            {
                //Print a red "Microphone not connected!" message at the center of the screen    
                GUI.contentColor = Color.red;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Microphone not connected!");
            }
        }
    }
  
    public void StopRecord()
        {
            float timeSinceStart = Time.time - startRecordingTime;
            if (!hasRecorded){
                Microphone.End(null); //Stop the audio recording    
                // goAudioSource.Play(); //Playback the recorded audio    
                hasRecorded = true;
                this.GetComponent<MeshRenderer>().material = materialRecorded;
            }
        }
}