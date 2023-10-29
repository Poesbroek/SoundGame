using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource safeCrackle;
    public AudioSource winSound;

    // Here we subscribe to all relevant events
    private void Start()
    {
        var safeDials = FindObjectsByType<SafeDial>(FindObjectsSortMode.None);
        foreach (var dial in safeDials)
        {
            dial.Safe.DialRotatedEvent += DialRotatedEventHandler;
            dial.Safe.UnlockedEvent    += SafeCrackedEventHandler;
        }
    }

    #region EventHandlers // These can also be provided through the Unity Editor!

    private void SafeCrackedEventHandler(object sender, EventArgs e)
    {
        Debug.Log("Stuff!");
        winSound.Play();
    }

    private void DialRotatedEventHandler(object sender, DialRotatedEventArgs e)
    {
        Debug.Log($"{e.WheelsRotated} wheels rotated!");
        safeCrackle.volume = e.WheelsRotated;
        safeCrackle.Play();
    }

    public void TimeLineTriggersThisEvent(string customText)
    {
        Debug.Log($"Signal received! Custom text: {customText}");
    }

    #endregion
}
