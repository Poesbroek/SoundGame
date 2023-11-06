using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFade : MonoBehaviour
{
    [SerializeField] AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] float timeUntilCurveFinished;
    [SerializeField] float maxVolume = 1;
    [SerializeField] AudioSource soundSource;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        float volume = Mathf.Lerp(0, 1, fadeCurve.Evaluate(timer / timeUntilCurveFinished));
        if(volume > 1) 
            volume = 1;
        soundSource.volume = volume * maxVolume;

        if(timer >= timeUntilCurveFinished) 
        { 
            this.enabled = false;
        }
    }
}
