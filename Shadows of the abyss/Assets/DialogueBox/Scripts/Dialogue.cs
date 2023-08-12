using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    string[] actualPhrase; 
    public GameObject speakingObject;
    public string[] phrases;
    public string[] phrasesOne;
    public string[] phrasesTwo;
    public string[] phrasesThree;
    public string[] phrasesFour;
    public string[] phrasesFive;
    public string[] phrasesSix;
    public string[] phrasesSeven;
    public string[] phrasesEight;
    public string[] phrasesNine;
    [SerializeField] public Text text;
    public bool isSpeaking;
    static string phrase = "";
    [SerializeField] GameObject panel;
    public static Dialogue self;
    public bool isChoosing = true;
    public int i = 0;
    public int k = 0;
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
        phrase = actualPhrase[k];
        if (i == phrase.Length && isSpeaking)
        {
            text.text = "";
            i = 0;
            isChoosing = true;
        }

        if (i < phrase.Length)
        {
            text.text += phrase[i];
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
                if (speakingObject.GetComponent<Dialogue>().phrases.Length != 0)
                    actualPhrase = speakingObject.GetComponent<Dialogue>().phrases;
                else ChoosePhrase();
                break;
            case 1:
                if (speakingObject.GetComponent<Dialogue>().phrasesOne.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesOne;
                else ChoosePhrase();
                break;
            case 2:
                if (speakingObject.GetComponent<Dialogue>().phrasesTwo.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesTwo;
                else ChoosePhrase();
                break;
            case 3:
                if (speakingObject.GetComponent<Dialogue>().phrasesThree.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesThree;
                else
                    ChoosePhrase();
                break;
            case 4:
                if (speakingObject.GetComponent<Dialogue>().phrasesFour.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesFour;
                else
                    ChoosePhrase();
                break;
            case 5:
                if (speakingObject.GetComponent<Dialogue>().phrasesFive.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesFive;
                else
                    ChoosePhrase();
                break;
            case 6:
                if (speakingObject.GetComponent<Dialogue>().phrasesSix.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesSix;
                else
                    ChoosePhrase();
                break;  
            case 7:
                if (speakingObject.GetComponent<Dialogue>().phrasesSeven.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesSeven;
                else
                    ChoosePhrase();
                break;
            case 8:
                if (speakingObject.GetComponent<Dialogue>().phrasesEight.Length != 0)
                     actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesEight;
                else
                    ChoosePhrase();
                break;
            case 9:
                if (speakingObject.GetComponent<Dialogue>().phrasesNine.Length != 0)
                    actualPhrase = speakingObject.GetComponent<Dialogue>().phrasesNine;
                else
                    ChoosePhrase();
                break;
        }
        isChoosing = false;
    }
}
