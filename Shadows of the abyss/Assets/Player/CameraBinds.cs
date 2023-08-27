using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBinds : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject inscMenu;
    public static CameraBinds self;
    public CameraBinds()
    {
        self = this;
    }
    bool isIPressed;
    bool isFPressed;
    void Update()
    {
        OpenInventory();
        DialogueBox();
    }
    void OpenInventory()
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
        if (Input.GetKeyUp(KeyCode.F) && isFPressed && !dialogue.activeSelf && PlayerScript.self.readyToSpeak && !SlotInteraction.isHovered)
        {
            isFPressed = false;
            dialogue.SetActive(true);
            Dialogue.isSpeaking = true;
            //Dialogue.self.speakingObject = PlayerScript.self.objectToSpeak;
        }
        else if (Input.GetKeyUp(KeyCode.F) && isFPressed && SlotInteraction.isHovered && !inscMenu.activeSelf) //+если кол-во слотов больше 0
        {
            IncriptionMenu();
        }
        if (Input.GetKeyDown(KeyCode.F) && dialogue.activeSelf)
            isFPressed = true;
        if ((Input.GetKeyUp(KeyCode.F) && isFPressed && dialogue.activeSelf || !PlayerScript.self.readyToSpeak) && !SlotInteraction.isHovered)
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
    void IncriptionMenu()
    {
        isFPressed = false;
        Description.isChoosingSlot = true;
        inscMenu.SetActive(true);
    }
    public void ExitIncriptionMenu()
    {
        Inventory.self.AcceptFeatures();
        Inventory.slots[SlotInteraction.hoveredId].canBeReplaced = true;
        inscMenu.SetActive(false);
    }
}
