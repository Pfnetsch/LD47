using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMercuryMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float initialMovementSpeed = 30F;
    private float _movementSpeed = 30F;

    public bool isJumpingAllowed = false;
    private bool movingToNextPlanet = false;

    // Increasing Speed - Mercury
    private int _movementDirection = 0; // 0 standing still, 1 is left, 2 is right
    private float mercurySize = 100f;
    
    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        // sync player vars
        bool isGrounded = _playerBody.GetComponent<Player>().isGrounded;
        
        if (movingToNextPlanet)
        {
            float step =  _movementSpeed * Time.deltaTime;
            Debug.Log("We Done");
            //player.transform.position = Vector3.MoveTowards(player.transform.position, nextPlanet.transform.position, step * 10F);
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

        _playerBody.gravityScale = 1 - (float)Math.Pow(_movementSpeed,2) / 6000F;
        
        /*
        if (_movementDirection != 0 && F < 1.1)
        {
            _movementSpeed += initialMovementSpeed * (1 + Time.deltaTime * 0.01F);
        }
        */

        /*
        if (Vector3.Distance(_playerBody.transform.position, gameObject.transform.position) > gameObject.GetComponent<Renderer>().bounds.size.x + 10f)
        {
            // lift off
            movingToNextPlanet = true;
            _playerBody.gravityScale = 0;
        }
        */
    }
    
    private void ResetMovementSpeed(int movementDirection)
    {
        _movementDirection = movementDirection;
        _movementSpeed = initialMovementSpeed;
    }
}
