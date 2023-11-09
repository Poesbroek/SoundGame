using UnityEngine;

public class EndingAudio : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_WinClip;
    [SerializeField] private AudioClip m_LoseClip;

    private bool done_playing = true;

    private void Awake()
    {
        if (GameStats.inTime)
        {
            m_AudioSource.PlayOneShot(m_WinClip);
            done_playing = false;
        }
        else
        {
            m_AudioSource.PlayOneShot(m_LoseClip);
            done_playing = false;
        }
    }

    private void Update()
    {
        if(m_AudioSource.isPlaying && !done_playing) 
        {
            m_AudioSource.Play();
            this.enabled = false;
        }
    }
}
