using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBinds : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool isPressed;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            isPressed = true;
        if (Input.GetKeyUp(KeyCode.I) && isPressed && !panel.activeSelf)
        {
            isPressed = false;
            panel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.I) && panel.activeSelf)
            isPressed = true;
        if(Input.GetKeyUp(KeyCode.I) && isPressed && panel.activeSelf)
        {
            panel.SetActive(false);
            isPressed = false;
        }

    }
}
