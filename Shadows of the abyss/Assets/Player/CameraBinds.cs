using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBinds : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject dialogue;
    bool isIPressed;
    bool isFPressed;
    void Update()
    {
        Inventory();
        DialogueBox();
    }
    void Inventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
            isIPressed = true;
        if (Input.GetKeyUp(KeyCode.I) && isIPressed && !panel.activeSelf)
        {
            isIPressed = false;
            panel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.I) && panel.activeSelf)
            isIPressed = true;
        if (Input.GetKeyUp(KeyCode.I) && isIPressed && panel.activeSelf)
        {
            panel.SetActive(false);
            isIPressed = false;
        }
    }
    void DialogueBox()
    {
        if (Input.GetKeyDown(KeyCode.F))
            isFPressed = true;
        if (Input.GetKeyUp(KeyCode.F) && isFPressed && !dialogue.activeSelf && PlayerScript.self.readyToSpeak)
        {
            isFPressed = false;
            dialogue.SetActive(true);
            Dialogue.isSpeaking = true;
            //Dialogue.self.speakingObject = PlayerScript.self.objectToSpeak;
        }
        if (Input.GetKeyDown(KeyCode.F) && dialogue.activeSelf)
            isFPressed = true;
        if (Input.GetKeyUp(KeyCode.F) && isFPressed && dialogue.activeSelf || !PlayerScript.self.readyToSpeak)
        {
            dialogue.SetActive(false);
            Dialogue.i = 0;
            Dialogue.k = 0;
            Dialogue.isChoosing = true;
            Dialogue.isSpeaking = false;
            Dialogue.self.text.text = "";
            isFPressed = false;
        }
     }
}
