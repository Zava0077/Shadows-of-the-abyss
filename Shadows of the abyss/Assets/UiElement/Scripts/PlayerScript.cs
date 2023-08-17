using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider2D;
    [SerializeField] public Rigidbody2D rb2d;
    public bool readyToSpeak;
    private Vector2 movePosition;
    //
    public float attackSpeed = 0.5f;
    //
    public int Speed = 5;
    public int Health;
    public int MaxHealth;
    public int Mana;
    public int MaxMana;
    public static PlayerScript self;
    public Camera Camera;
    public GameObject game;
    public GameObject game2;
    public PlayerScript() { self = this; }

    void Awake()
    {
        collider2D = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movePosition = moveInput * Speed;
        Rect screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
        if(screenRect.Contains(Input.mousePosition))
        {
            Vector2 relativeMousePosition = transform.position + (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height* 0.5f));
            Camera.transform.position = Vector2.Lerp(transform.position, relativeMousePosition*2, 0.002f);
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movePosition * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InteractionObject")
        {
            readyToSpeak = true;
            Dialogue.self.speakingObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "InteractionObject")
        {
            readyToSpeak = false;
            Dialogue.isSpeaking = false;
            Dialogue.self.speakingObject = null;
        }
    }
}
