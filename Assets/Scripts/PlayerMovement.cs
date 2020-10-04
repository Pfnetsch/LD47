using Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    public GameObject currentPlanet;
    public GameObject nextPlanet;
    public Transform allPlanets;

    public Transform leftBorder;
    public Transform rightBorder;

    public float initialMovementSpeed = 0.2F;
    private float _movementSpeed;

    public bool isJumpingAllowed = false;
    private bool movingToNextPlanet = false;

    // Increasing Speed - Mercury
    private int _movementDirection = 0; // 0 standing still, 1 is left, 2 is right

    // Direction to the next Planet


    // Jumping
    private Rigidbody2D _playerBody;

    // Start is called before the first frame update
    void Start()
    {
        _movementSpeed = initialMovementSpeed;
        _playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToNextPlanet)
        {
            float step =  _movementSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(
                player.transform.position, nextPlanet.transform.position, step * 10F);
        }
        
        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_movementDirection != 1)
            {
                ResetMovementSpeed(1);
            }

            // Rotate Planet
            allPlanets.RotateAround(currentPlanet.transform.position, Vector3.forward, _movementSpeed * -1F);
        }
        else if (_movementDirection == 1)   // Left Key Up
        {
            ResetMovementSpeed(0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_movementDirection != 2)
            {
                ResetMovementSpeed(2);
            }

            // Rotate Planet
            allPlanets.RotateAround(currentPlanet.transform.position, Vector3.forward, _movementSpeed);
        }
        else if (_movementDirection == 2)
        {
            ResetMovementSpeed(0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Player>().isGrounded)
        {
            currentPlanet.GetComponent<PlayerPlanetMovement>().Jump();
        }
        
        // calculate centrifugal forces
        // F = m * v^2/r
        float r = currentPlanet.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        double F = Math.Pow(_movementSpeed, 2.0) / r;

        _playerBody.gravityScale = 1 - (float)F;
        
        if (_movementDirection != 0 && F < 1.1)
        {
            _movementSpeed += initialMovementSpeed * (1 + Time.deltaTime * 0.01F);
        }

        if (Vector3.Distance(_playerBody.transform.position, currentPlanet.transform.position) > currentPlanet.GetComponent<Renderer>().bounds.size.x + 10f)
        {
            // lift off
            movingToNextPlanet = true;
            _playerBody.gravityScale = 0;
            Debug.Log("HERE");
        }
    }

    private void ResetMovementSpeed(int movementDirection)
    {
        _movementDirection = movementDirection;
        _movementSpeed = initialMovementSpeed;
    }
}
