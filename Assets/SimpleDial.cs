using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleDial : MonoBehaviour
{
    int currentNumber = 0;
    [SerializeField] int maxNumber = 10;

    [SerializeField] int steps;
    int step = 0;
    int highestStep = 0; // What is the furthest step the player has gotten to?
    int[] code;

    [SerializeField] AudioClip click;
    [SerializeField] AudioClip altClick;
    [SerializeField] AudioSource clickSource;

    [SerializeField] AudioSource ambienceSource;

    [SerializeField] UnityEvent winEvent;

    private void Start()
    {
        code = GenerateCode(steps);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ChangeNumber(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeNumber(-1);
    }

    private void ChangeNumber(int delta)
    {
        if ((delta > 0 && !isEven(step)) || (delta < 0 && isEven(step) && step > 0))
        {
            step--;
            Debug.Log("Moved down to step " + step);
        }

        currentNumber += delta;
        if (currentNumber >= maxNumber)
            currentNumber = 0;
        else if (currentNumber < 0)
            currentNumber = maxNumber - 1;

        Debug.Log("Current digit: " + currentNumber);
        Debug.Log(step);

        if (currentNumber == code[step])
        {
            clickSource.pitch = 1;
            clickSource.PlayOneShot(altClick);
            step++;
            Debug.Log("Moved up to step " + step);
            if (step >= code.Length)
            {
                Debug.Log("The safe is open!");
                StopAllCoroutines();
                winEvent.Invoke();
            }
        }
        else
        {
            clickSource.pitch = 1 + step * 0.1f;
            clickSource.PlayOneShot(click);
        }

        if (step > highestStep)
        {
            highestStep = step;

            if (highestStep < code.Length)
                StartCoroutine("constructionTimer");
        }
    }

    private bool isEven(int n) => n % 2 == 0;

    private IEnumerator constructionTimer()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 2.2f));
        ambienceSource.volume += 1f / steps;
        Debug.Log("Volume: " + ambienceSource.volume);
    }

    private int[] GenerateCode(int length)
    {
        int[] result = new int[length];
        for (int i = 0; i < length; i++)
            result[i] = Random.Range(0, maxNumber);

        return result;
    }
}