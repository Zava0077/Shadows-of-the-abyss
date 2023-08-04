using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int countMonsters = 1;
    public GameObject bat;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(PlayerScript.self.transform.position, transform.position) <= 3)
        {
            switch (Random.Range(1, countMonsters))
            {
                case 1:
                    {
                        //bat
                        Instantiate(bat,this.transform.position, new Quaternion(),transform.parent);
                        Destroy(gameObject);
                        break;
                    }
            }
        }
    }
}
