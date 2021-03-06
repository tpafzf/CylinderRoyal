﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    private Transform playerTransform;
    private Rigidbody playerRigidBody;
    private Vector3 yRotation;
    private float rotateSpeed;
    private float moveSpeed;
    private Vector3 translation;
    private bool jumped;
    private Vector3 jumpForceVector;
    private float jumpForce;

    private Vector3 standVector;
    private Vector3 crouchVector;

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, -20f, 0);
        playerTransform = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
        jumpForce = 500f;
        jumpForceVector = new Vector3(0, jumpForce, 0);
        Debug.Log(playerTransform);
        yRotation = new Vector3(0, 0, 0);
        translation = new Vector3(0, 0, 0);
        rotateSpeed = 100f;
        moveSpeed = 10f;
        jumped = false;

        standVector = new Vector3(0.5778568f, 0.7373421f, 0.5068351f);
        crouchVector = new Vector3(0.5778568f, 0.4373421f, 0.5068351f);
    }

    // Update is called once per frame
    void Update () {

        // rotate with mouse
        yRotation.y = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
        playerTransform.Rotate(yRotation);


        // crouch
        if (Input.GetKey(KeyCode.C))
        {
            playerTransform.localScale = crouchVector;
            moveSpeed = 3f;
        } else
        {
            playerTransform.localScale = standVector;
            moveSpeed = 10f;
        }

        // adjust speed with shift key
        if (Input.GetKey(KeyCode.LeftShift) && !(Input.GetKey(KeyCode.C)))
        {
            moveSpeed = 20f;
        } else
        {
            moveSpeed = 10f;
        }

        // move with wasd
        float deltaZ = 0;
        float deltaX = 0;

        if (Input.GetKey(KeyCode.A)) deltaZ -= moveSpeed;
        if (Input.GetKey(KeyCode.D)) deltaZ += moveSpeed;
        if (Input.GetKey(KeyCode.W)) deltaX -= moveSpeed;
        if (Input.GetKey(KeyCode.S)) deltaX += moveSpeed;

        translation.x = deltaX;
        translation.z = deltaZ;

        playerTransform.Translate(translation * Time.deltaTime);

        // handle jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }

	}

    void FixedUpdate ()
    {
        if (jumped)
        {
            playerRigidBody.AddForce(jumpForceVector);
            jumped = false;
        }
    }
}
