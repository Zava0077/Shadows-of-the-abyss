using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class NPC : MonoBehaviour
{
    string[] actualPhrase;
    private GameObject speakingObject;
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
    public static bool isSpeaking;
    static string phrase = "";
    [SerializeField] GameObject panel;
    public static NPC self;
    public static bool isChoosing = true;
    public NPC()
    {
        self = this;
    }
    private void Awake()
    {
        isChoosing = true;
    }
}
