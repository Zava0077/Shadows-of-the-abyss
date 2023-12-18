using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static Usable;

public class PrefixChangerObject : MonoBehaviour //Общий класс на все особые Usable предметы, не только Prefix
{
    Vector2 mouse;
    public Sprite curSprite;
    public GameObject original;
    public UsableEvent @event;
    public static PrefixChangerObject _ref;
    public static int _id = 0;
    public PrefixChangerObject()
    {
        _ref = this;
    }
    private void Start()
    {
        if(curSprite)
            GetComponent<SpriteRenderer>().sprite = curSprite;
    }
    private void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouse;
    }
    public void ClickEvent(GameObject gO, int id = -1)
    {
        _id = id;
        @event(gO);
        CursorSlot.self.enabled = true;
        Description.self.enabled = true;
        Destroy(this.gameObject);
    }
}
