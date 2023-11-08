using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Tutorial : MonoBehaviour
{
    public AudioClip[] lines;
    private bool _progress;

    private int _currentAudioClip = -1;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(TutorialFlow());
    }

    private IEnumerator TutorialFlow()
    {
        SimpleDial.AllowedInput = 0;
        yield return PlayNextAudio(); // Woosh

        for (int i = 0; i < 3; i++) // Play audio, and wait until safe is cracked, three times
        {
            yield return PlayNextAudio();
            SimpleDial.AllowedInput = (AllowedInput)(i % 2 + 1);
            yield return new WaitUntil(() => _progress);
            SimpleDial.AllowedInput = 0;
            _progress = false;
        }

        yield return PlayNextAudio();

        yield return new WaitForSeconds(.4f);
        yield return PlayNextAudio(); // Woosh

        // Load next scene
        SimpleDial.AllowedInput = AllowedInput.Left | AllowedInput.Right;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator PlayNextAudio()
    {
        _audioSource.PlayOneShot(lines[++_currentAudioClip]);
        yield return new WaitWhile(() => _audioSource.isPlaying);
    }

    public void SafeProgressed() => _progress = true; // Triggered by UnityEvent
}
