using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField]
    private float sens;
    [SerializeField]
    private Transform orientation;

    private float mouseX;
    private float mouseY;

    void Start()
    {
        orientation = GameObject.Find("Orientation").transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        mouseY += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);
        orientation.rotation = Quaternion.Euler(0f, mouseX, 0f);

    }
}
