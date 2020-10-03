using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform planet;
    public Transform player;
    public Transform leftBorder;
    public Transform rightBorder;
    public float movementSpeed = 0.5F;

    private bool isGrounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (player.position.x > leftBorder.position.x && (player.position.y > leftBorder.position.y || player.position.x > 0F))
            {
                // rotate player
                player.RotateAround(planet.position, Vector3.forward, movementSpeed);
            }
            else
            {
                // rotate planet
                planet.RotateAround(planet.position, Vector3.forward, movementSpeed * -1F);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (player.position.x < rightBorder.position.x && (player.position.y > rightBorder.position.y || player.position.x < 0F))
            {
                // rotate player
                player.RotateAround(planet.position, Vector3.forward, movementSpeed * -1F);
            }
            else
            {
                // rotate planet
                planet.RotateAround(planet.position, Vector3.forward, movementSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Jump
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
