using Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;

    public Transform currentPlanet;
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
        if (Input.GetKey(KeyCode.LeftArrow) && player.GetComponent<Player>().isGrounded)
        {
            if (_movementDirection != 1)
            {
                ResetMovementSpeed(1);
            }

            if (player.transform.position.x > leftBorder.position.x && (player.transform.position.y > leftBorder.position.y || player.transform.localPosition.x > 0F))
            {
                // rotate player
                player.transform.RotateAround(currentPlanet.position, Vector3.forward, _movementSpeed);
            }
            else
            {
                // rotate planet
                allPlanets.RotateAround(currentPlanet.position, Vector3.forward, _movementSpeed * -1F);
            }
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

            if (player.transform.position.x < rightBorder.position.x && (player.transform.position.y > rightBorder.position.y || player.transform.localPosition.x < 0F))
            {
                // rotate player
                player.transform.RotateAround(currentPlanet.position, Vector3.forward, _movementSpeed * -1F);
            }
            else
            {
                // rotate planet
                allPlanets.RotateAround(currentPlanet.position, Vector3.forward, _movementSpeed);
            }
        }
        else if (_movementDirection == 2)
        {
            ResetMovementSpeed(0);
        }

        if (isJumpingAllowed && Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Player>().isGrounded)
        {
            _playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
        }

        _movementTimer += Time.deltaTime;

        if (_movementDirection != 0 && moveIncrDelayInSec != 0 && _movementTimer > moveIncrDelayInSec)
        {
            _movementSpeed += initialMovementSpeed * _movementIncrementCounter * _movementIncrementCounter;
            _playerBody.gravityScale -= 0.01F * _movementIncrementCounter * _movementIncrementCounter;
            _movementTimer = 0.0F;
            _movementIncrementCounter++;

            UnityEngine.Debug.Log("GravityScale: " + _playerBody.gravityScale);
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
