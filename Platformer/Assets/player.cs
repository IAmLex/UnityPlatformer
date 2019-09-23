using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody rigidbody;

    public float movementSpeed;
    public float jumpHeight;
    public Vector2 movement;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

        movementSpeed = 4.5f;
        jumpHeight = 8f;

        isGrounded = true;

        Physics.gravity = new Vector3(0, -18.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (Input.GetAxis("Horizontal") != 0)
        {
            rigidbody.MovePosition(transform.position + new Vector3(Mathf.Sign(Input.GetAxis("Horizontal")) * Time.fixedDeltaTime * movementSpeed, 0, 0));
            //rigidbody.MovePosition(new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0, 0));
        }

        if (Input.GetAxis("Jump") != 0  && isGrounded)
        {
            isGrounded = false;

            rigidbody.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        rigidbody.MovePosition((Vector2)transform.position + (direction * movementSpeed * Time.deltaTime));
    }

    private void OnCollisionStay()
    {
        if (!isGrounded && rigidbody.velocity.y == 0)
        {
            isGrounded = true;
        }
    }
}
