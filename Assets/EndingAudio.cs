using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAudio : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_WinClip;
    [SerializeField] private AudioClip m_LoseClip;

    private bool toggle = false;
    private void Awake()
    {
        toggle = !toggle;
        if (GameStats.inTime)
            m_AudioSource.PlayOneShot(m_WinClip);
        else
            m_AudioSource.PlayOneShot(m_LoseClip);
    }

    private void Update()
    {
        if (!m_AudioSource.isPlaying && toggle) 
        { 
            m_AudioSource.Play();
            this.enabled = false;
        }
    }

}
