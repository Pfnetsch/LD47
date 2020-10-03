using Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;

    public GameObject currentPlanet;
    public Transform nextPlanet;
    public Transform allPlanets;

    public Transform leftBorder;
    public Transform rightBorder;

    public float initialMovementSpeed = 0.2F;
    private float _movementSpeed;

    public bool isJumpingAllowed = false;


    // Increasing Speed - Mercury
    public float moveIncrDelayInSec = 2.0F;
    private int _movementDirection = 0; // 0 standing still, 1 is left, 2 is right
    private float _movementTimer = 0.0F;
    private int _movementIncrementCounter = 1;


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
        // move left
        if (Input.GetKey(KeyCode.LeftArrow) && player.GetComponent<Player>().isGrounded)
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

        if (Input.GetKey(KeyCode.RightArrow) && player.GetComponent<Player>().isGrounded)
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
    }

    private void FixedUpdate()
    {
        _movementTimer += Time.deltaTime;

        if (_movementDirection != 0 && moveIncrDelayInSec != 0 && _movementTimer > moveIncrDelayInSec)
        {
            _movementSpeed += initialMovementSpeed * _movementIncrementCounter * _movementIncrementCounter;
            _playerBody.gravityScale -= 0.01F * _movementIncrementCounter * _movementIncrementCounter;
            _movementTimer = 0.0F;
            _movementIncrementCounter++;
        }
    }

    private void ResetMovementSpeed(int movementDirection)
    {
        _movementDirection = movementDirection;
        _movementSpeed = initialMovementSpeed;
        _movementTimer = 0.0f; // Timer Reset
        _movementIncrementCounter = 1;
    }
}
