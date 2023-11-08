using UnityEngine;

public class EndingAudio : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_WinClip;
    [SerializeField] private AudioClip m_LoseClip;

    private void Awake()
    {
        m_AudioSource.Play();
        if (GameStats.inTime)
        {
            m_AudioSource.PlayOneShot(m_WinClip);
        }
        else
        {
            m_AudioSource.PlayOneShot(m_LoseClip);
        }
    }
}
