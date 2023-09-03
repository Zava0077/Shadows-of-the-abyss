using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
        gameObject.GetComponentsInChildren<Text>()[0].color = new Color(gameObject.GetComponentsInChildren<Text>()[0].color.r, gameObject.GetComponentsInChildren<Text>()[0].color.g, gameObject.GetComponentsInChildren<Text>()[0].color.b, gameObject.GetComponentsInChildren<Text>()[0].color.a - Time.deltaTime);
        gameObject.GetComponentsInChildren<Text>()[1].color = new Color(gameObject.GetComponentsInChildren<Text>()[1].color.r, gameObject.GetComponentsInChildren<Text>()[1].color.g, gameObject.GetComponentsInChildren<Text>()[1].color.b, gameObject.GetComponentsInChildren<Text>()[1].color.a - Time.deltaTime);
        if (gameObject.GetComponentInChildren<Text>().color.a <= 0)
            Destroy(gameObject);
    }
}
