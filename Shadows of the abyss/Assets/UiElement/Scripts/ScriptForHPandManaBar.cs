using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForHPandManaBar : MonoBehaviour
{
    [SerializeField] private Image HpBar;
    [SerializeField] private Image ManaBar;
    [SerializeField] private Text HealthText;
    [SerializeField] private Text ManaText;
    private int MaxHealth;
    private int MaxMana;
    private int Health;
    private int Mana;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MaxHealth = PlayerScript.self.MaxHealth;
        MaxMana = PlayerScript.self.MaxMana;
        Health = PlayerScript.self.Health;
        Mana = PlayerScript.self.Mana;
        HealthText.text = Health.ToString();
        ManaText.text = Mana.ToString();
        //345 width max and 175 pos.x max
        HpBar.fillAmount = (float)((double)Health / (double)MaxHealth);
        ManaBar.fillAmount = (float)((double)Mana / (double)MaxMana);
    }
}
