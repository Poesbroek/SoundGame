using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDial : MonoBehaviour
{
    int currentNumber = 0;
    [SerializeField] int maxNumber = 10;

    [SerializeField] int[] code;
    int step = 0;

    [SerializeField] AudioClip click;
    [SerializeField] AudioClip altClick;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (delta > 0 && !isEven(step))
            step--;
        else if (delta < 0 && isEven(step) && step > 0)
            step--;

        currentNumber += delta;
        if (currentNumber >= maxNumber)
            currentNumber = 0;
        else if (currentNumber < 0)
            currentNumber = maxNumber - 1;

        Debug.Log(currentNumber);

        if (currentNumber == code[step])
        {
            audioSource.PlayOneShot(altClick);
            Debug.Log("Yes, good");
            step++;
            if (step >= code.Length)
                Debug.Log("YOU DID IT!!!!!121111");
        }
        else
        {
            audioSource.PlayOneShot(click);
        }
    }

    private bool isEven(int n) => n % 2 == 0;
}
