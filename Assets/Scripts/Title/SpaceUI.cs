using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceUI : MonoBehaviour
{
    [SerializeField]
    private GameObject SpaceUIObj;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpaceUIObj.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(this);
        }
    }
}
