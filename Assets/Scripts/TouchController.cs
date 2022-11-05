using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private DynamicJoystick dJoystick;
    public float speed;
    public float rotationSpeed;
    private float horizontalInput;
    private float forwardInput;
    Rigidbody rb;
    Vector3 moveToDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dJoystick = FindObjectOfType<DynamicJoystick>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        //Debug.Log(horizontalInput);
        if (Input.touchCount>0)
        {

            PlayerMovement();
        }
    }
    void PlayerMovement()
    {
        horizontalInput = dJoystick.Horizontal;
        forwardInput = dJoystick.Vertical;
        moveToDirection = new Vector3(horizontalInput, 0, forwardInput);
        moveToDirection.Normalize();
        transform.Translate(moveToDirection * speed * Time.deltaTime, Space.World);
        if (moveToDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveToDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            for (int i = 0; i < HumanStack.instance.humanList.Count; i++)
            {
                if (HumanStack.instance.humanList.Count>=1)
                {
                    HumanStack.instance.humanList[i].transform.rotation= Quaternion.RotateTowards(HumanStack.instance.humanList[i].transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
