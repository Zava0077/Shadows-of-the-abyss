using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    string[] actualPhrase; 
    public GameObject speakingObject;
    [SerializeField] public Text text;
    public static bool isSpeaking;
    static string phrase = "";
    [SerializeField] GameObject panel;
    public static Dialogue self;
    public static bool isChoosing = true;
    public static int i = 0;
    public static int k = 0;
    public Dialogue()
    {
        self = this;
    }
    private void Awake()
    {
        isChoosing = true;
    }
    private void Update()
    {
        if (isSpeaking)
            Invoke(nameof(Speak), 0.05f);
        NextPhrase();
    }
    public void Speak()
    {
        if(isChoosing)
            ChoosePhrase();
        phrase = actualPhrase[k];//
        if (i == phrase.Length && isSpeaking)
        {
            text.text = "";
            i = 0;
            isChoosing = true;
        }

        if (i < phrase.Length)
        {
            text.text += phrase[i];//
            i++;
        }
        if(i==phrase.Length)
        {
            isSpeaking = false;
        }
        CancelInvoke(nameof(Speak));
    }
    public void NextPhrase()
    {
        if (Input.GetButtonDown("Jump"))
        {
            k++;
            if (k > actualPhrase.Length - 1)
            {
                k = 0;
                panel.SetActive(false);
                isChoosing = true;
            }
            isSpeaking = true;
            text.text = "";
            i = 0;
        }

    }
    void ChoosePhrase()
    {
        switch (Random.Range(0, 9))
        {
            case 0:
                if (speakingObject.GetComponent<NPC>().phrases.Length != 0)
                    actualPhrase = speakingObject.GetComponent<NPC>().phrases;
                else ChoosePhrase();
                break;
            case 1:
                if (speakingObject.GetComponent<NPC>().phrasesOne.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesOne;
                else ChoosePhrase();
                break;
            case 2:
                if (speakingObject.GetComponent<NPC>().phrasesTwo.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesTwo;
                else ChoosePhrase();
                break;
            case 3:
                if (speakingObject.GetComponent<NPC>().phrasesThree.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesThree;
                else
                    ChoosePhrase();
                break;
            case 4:
                if (speakingObject.GetComponent<NPC>().phrasesFour.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesFour;
                else
                    ChoosePhrase();
                break;
            case 5:
                if (speakingObject.GetComponent<NPC>().phrasesFive.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesFive;
                else
                    ChoosePhrase();
                break;
            case 6:
                if (speakingObject.GetComponent<NPC>().phrasesSix.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesSix;
                else
                    ChoosePhrase();
                break;  
            case 7:
                if (speakingObject.GetComponent<NPC>().phrasesSeven.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesSeven;
                else
                    ChoosePhrase();
                break;
            case 8:
                if (speakingObject.GetComponent<NPC>().phrasesEight.Length != 0)
                     actualPhrase = speakingObject.GetComponent<NPC>().phrasesEight;
                else
                    ChoosePhrase();
                break;
            case 9:
                if (speakingObject.GetComponent<NPC>().phrasesNine.Length != 0)
                    actualPhrase = speakingObject.GetComponent<NPC>().phrasesNine;
                else
                    ChoosePhrase();
                break;
        }
        isChoosing = false;
    }
}
