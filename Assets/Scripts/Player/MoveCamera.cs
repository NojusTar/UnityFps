using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    
    [SerializeField]
    private Transform camPosition;

    private void Start()
    {
        camPosition = GameObject.Find("CamPos").transform;
    }

    void Update()
    {
        transform.position = camPosition.position;
    }
}
