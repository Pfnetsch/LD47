﻿using MiscUtil.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEarthMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 0.1F;

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        Debug.Log("Earth Movement");

        if (playerBody.GetComponent<Player>().isUnderWater)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerBody.transform.Rotate(new Vector3(0F, 0F, 0.2F));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerBody.transform.Rotate(new Vector3(0F, 0F, -0.2F));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerBody.AddRelativeForce(new Vector2(0.0F, 0.8F), ForceMode2D.Force);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerBody.AddRelativeForce(new Vector2(0.0F, -0.8F), ForceMode2D.Force);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && playerBody.GetComponent<Player>().isGrounded)
            {
                playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
            }
        }
    }
}