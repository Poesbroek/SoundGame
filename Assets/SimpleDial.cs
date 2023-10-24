using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleDial : MonoBehaviour
{
    int currentNumber = 0;
    [SerializeField] int maxNumber = 10;

    [SerializeField] int[] code;
    int step = 0;

    [SerializeField] AudioClip click;
    [SerializeField] AudioClip altClick;
    [SerializeField] AudioSource clickSource;

    [SerializeField] AudioSource ambienceSource;

    [SerializeField] UnityEvent winEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ChangeNumber(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeNumber(-1);
    }

    private void ChangeNumber(int delta)
    {
        if (delta > 0 && !isEven(step))
            step--;
        else if (delta < 0 && isEven(step) && step > 0)
            step--;

        currentNumber += delta;
        if (currentNumber >= maxNumber)
            currentNumber = 0;
        else if (currentNumber < 0)
            currentNumber = maxNumber - 1;

        Debug.Log("Current digit: " + currentNumber);

        if (currentNumber == code[step])
        {
            clickSource.PlayOneShot(altClick);
            step++;
            Debug.Log("Moved on to step " + step);
            if (step >= code.Length)
            {
                Debug.Log("The safe is open!");
                winEvent.Invoke();
            }
        }
        else
        {
            clickSource.PlayOneShot(click);
        }

        if (step == 2 && !ambienceSource.isPlaying)
            StartCoroutine("constructionTimer");
    }

    private bool isEven(int n) => n % 2 == 0;

    private IEnumerator constructionTimer()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        ambienceSource.Play();
    }
}