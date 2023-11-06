using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TitleScreen : MonoBehaviour
{
    public AudioClip ringtone;
    public AudioClip theTalk;

    private bool _playing;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(ringtone);
    }

    void Update()
    {
        if (_playing) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return; // Also just to be sure
        }

        if (Input.anyKeyDown) // Aside from Escape
        {
            _playing = true;
            StartCoroutine(PickUpPhone());
        }
    }

    private IEnumerator PickUpPhone()
    {
        _audioSource.Stop();
        // TODO answer phone sound
        yield return new WaitForSeconds(1);
        _audioSource.PlayOneShot(theTalk);
        yield return new WaitWhile(() => _audioSource.isPlaying);
        SceneManager.LoadScene(1); // Assumes this is scene 0. Quick and dirty.
    }
}
