using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMercuryMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float initialMovementSpeed = 30F;
    private float _movementSpeed = 30F;

    public bool isJumpingAllowed = false;
    private bool movingToNextPlanet = false;

    // Increasing Speed - Mercury
    private int _movementDirection = 0; // 0 standing still, 1 is left, 2 is right
    private float mercurySize = 100f;

    private float timeOnGround = 0;
    
    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        // sync player vars
        bool isGrounded = _playerBody.GetComponent<Player>().isGrounded;

        if (isGrounded)
        {
            timeOnGround++;
        }
        else
        {
            timeOnGround = 0;
        }

        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // speed up if player is grounded
            if (isGrounded)
            {
                _movementSpeed *= 1 + ( 0.2F * Time.deltaTime);
            }
            else
            {
                _movementSpeed *= 1 - ( 0.1F * Time.deltaTime);
            }
            
            // reset speed if movement direction is changed
            if (_movementDirection != 1)
            {
                ResetMovementSpeed(1);
            }

            // move
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * Time.deltaTime * -1F);
            
        }
        else if (_movementDirection == 1)   // Left Key Up
        {
            ResetMovementSpeed(0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // speed up if player is grounded
            if (isGrounded)
            {
                _movementSpeed *= 1 + ( Time.deltaTime);
            }
            else
            {
                _movementSpeed *= 1 - ( 0.2F * Time.deltaTime);
            }
            
            // reset speed if movement direction is changed
            if (_movementDirection != 2)
            {
                ResetMovementSpeed(2);
            }

            // move
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * Time.deltaTime);
        }
        else if (_movementDirection == 2)
        {
            ResetMovementSpeed(0);
        }

        // calculate centrifugal forces
        // F = m * v^2/r
        float r = mercurySize;
        double F = Math.Pow(_movementSpeed, 2.0) / r;

        if (timeOnGround > 400 || (float)Math.Pow(_movementSpeed,2) / 6000F < 1.0F)
        {
            _playerBody.gravityScale = 1 - (float)Math.Pow(_movementSpeed,2) / 6000F;
        }

        if (_movementSpeed > 1000)
        {
            movingToNextPlanet = true;
        }

        Debug.Log(Vector3.Distance(_playerBody.transform.position, gameObject.transform.position));
        
        if (Vector3.Distance(_playerBody.transform.position, gameObject.transform.position) > 35f)
        {
            // lift off
            movingToNextPlanet = true;
            GlobalInformation.currentScene++;
            SceneManager.LoadScene("Transition");
        }
        
    }
    
    private void ResetMovementSpeed(int movementDirection)
    {
        _movementDirection = movementDirection;
        _movementSpeed = initialMovementSpeed;
    }
}
