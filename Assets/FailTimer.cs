using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FailTimer : MonoBehaviour
{
    [SerializeField] float secondsBeforeFail;
    [SerializeField] UnityEvent Failed;

    private void Awake()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(secondsBeforeFail);
        GameStats.inTime = false;
        Failed.Invoke();
    }
}
