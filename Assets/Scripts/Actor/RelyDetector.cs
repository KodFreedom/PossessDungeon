using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelyDetector : MonoBehaviour
{
    private GameObject target_ = null;
    public GameObject Target { get { return target_; } }

    // Use this for initialization
    void Start()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        var collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(0.0f, 0.0f, 5.0f);
        collider.size = new Vector3(2.0f, 5.0f, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Actor"))
        {
            target_ = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Actor")
            && other.gameObject == target_)
        {
            target_ = null;
        }
    }
}
