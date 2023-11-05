using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller c;

    private void Start()
    {
        c = this;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
