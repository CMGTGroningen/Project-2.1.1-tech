//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;

public class play_stop_audio : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    public void togglePlay()
    {
        //Check if the audiosource is already playing
        if (m_MyAudioSource.isPlaying)
        {
            //Play the audio you attach to the AudioSource component
            m_MyAudioSource.Stop();
        }
        else
        {
            //Play the audio
            m_MyAudioSource.Play();
        }
    }
}