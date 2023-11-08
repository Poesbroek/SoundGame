using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectEnding : MonoBehaviour
{
    [SerializeField] int MemeEndingChance = 100;
    [SerializeField] private string mainSceneName;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip normalClip;
    [SerializeField] private AudioClip memeClip;

    private bool audioToggle = false
        ;
    private void Awake()
    {
        float random = Random.Range(0, MemeEndingChance);
        if(random == MemeEndingChance) 
        {
            audioSource.PlayOneShot(memeClip);
            audioToggle = true;
        }
        else
        {
            audioSource.PlayOneShot(normalClip);
            audioToggle = true;
        }
    }
    private void Update()
    {
        if(audioToggle && !audioSource.isPlaying) 
        {
            SceneManager.LoadScene(mainSceneName);
            this.enabled = false;
        }
    }

}
