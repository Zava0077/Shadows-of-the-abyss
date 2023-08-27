using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;
using System;
using static UnityEngine.GraphicsBuffer;
using TMPro;
using UnityEditor.UIElements;

public class PlayerScript : Creature
{
    [SerializeField] private BoxCollider2D collider2D;
    [SerializeField] private Rigidbody2D rb2d;
    private Vector2 movePosition;
    public int Mana;
    public int MaxMana = 200;
    public static PlayerScript self;
    float _oneSecTimer = 0;
    public Camera Camera;
    public PlayerScript() 
    {
        MaxHealth = 100;
        Health = 100;
        Mana = 200;
        Speed = 5;

        FireRes = 0;
        ColdRes = 0;
        LightningRes = 0;
        PhysicalRes = 0;
        PoisonRes = 0;
        VoidRes = 0;

        Armor = 0;
        Evasion = 0;

        self = this; 
    }

    void Awake()
    {
        collider2D = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Limits();
        #region Камера
        Vector2 MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movePosition = moveInput * Speed;
        Rect screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
        if(screenRect.Contains(Input.mousePosition))
        {
            Vector2 relativeMousePosition = transform.position + (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height* 0.5f));
            Camera.transform.position = Vector2.Lerp(transform.position, relativeMousePosition*2, 0.002f);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movePosition * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _oneSecTimer += Time.deltaTime;
        if (_oneSecTimer >= 1f)
        {
            _oneSecTimer -= 1f;
            DamageType.GetDamage(collision.GetComponent<Creature>(), 0, DamageType.DamageTypes.Physical);
        }
    }
}
