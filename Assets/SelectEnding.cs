using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectEnding : MonoBehaviour
{
    [SerializeField] int MemeEndingChance = 100;
    [SerializeField] UnityEvent normalEnding;
    [SerializeField] UnityEvent memeEnding;

    private void Awake()
    {
        float random = Random.Range(0, MemeEndingChance);
        if (random != MemeEndingChance)
            normalEnding.Invoke();
        else 
            memeEnding.Invoke();

    }
}
