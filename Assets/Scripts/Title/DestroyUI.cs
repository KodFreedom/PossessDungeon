using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUI : MonoBehaviour
{
    [SerializeField]
    private GameObject Golem;

    [SerializeField]
    private bool UIFlag;

    [SerializeField]
    private GameObject Collibox;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UIFlag == false)
        {
            if (Golem.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        else if (UIFlag == true)
        {
            if (Input.GetButton("Jump"))
            {
                if (Golem.tag == "Npc")
                {
                    Destroy(Collibox);
                    Destroy(gameObject);
                }
            }
        }
    }
}
