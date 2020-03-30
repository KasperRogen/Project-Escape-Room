using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : NetworkBehaviour
{
    CharacterController cc;

    public float speed = 12f;

    public float MouseSensitivity = 100f;

    public Transform camPos;

    Transform cam;

    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if (base.hasAuthority && base.isClient)
        {
            cc = GetComponent<CharacterController>();

            if (base.isLocalPlayer)
            {
                cam = Camera.main.transform;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (base.hasAuthority && base.isClient)
        {
            Move();
            Look();
        }
            

    }

    private void Look()
    {
        Vector2 MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.Rotate(transform.up * MouseInput.x);

        cam.rotation = transform.rotation;
        cam.Rotate(new Vector3(xRotation, 0, 0));
        cam.position = camPos.position;
    }

    private void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.right * input.x + transform.forward * input.z;

        cc.Move(move * speed * Time.deltaTime);
    }
}
