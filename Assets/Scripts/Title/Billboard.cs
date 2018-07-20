using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform ParentPos;
    private Transform targetToFace;
    Quaternion adjustEuler = Quaternion.Euler(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        if (targetToFace == null)
        {
            var mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
            targetToFace = mainCameraObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetToFace != null)
        {
            transform.position = new Vector3 (ParentPos.position.x + -1.2f, ParentPos.position.y + 2.2f, ParentPos.position.z);
            transform.rotation = targetToFace.rotation; 
        }
    }
}
