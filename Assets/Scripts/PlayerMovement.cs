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

    public float movementSpeed = 0.5F;
    public bool isJumpingAllowed = false;


    // Increasing Speed - Mercury
    public float moveIncrDelayInSec = 2.0F;
    private int _movementDirection = 0; // 0 standing still, 1 is left, 2 is right
    private float _movementTimer = 0.0F;
    private int _incCounter = 1;

    // Direction to the next Planet


    // Jumping
    private Rigidbody2D _playerBody;

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_movementDirection != 1)
            {
                _movementTimer = 0.0f; // Timer Reset
                _movementDirection = 1;
            }

            if (player.transform.position.x > leftBorder.position.x && (player.transform.position.y > leftBorder.position.y || player.transform.position.x > 0F))
            {
                // rotate player
                player.transform.RotateAround(currentPlanet.position, Vector3.forward, movementSpeed);
            }
            else
            {
                // rotate planet
                allPlanets.RotateAround(currentPlanet.position, Vector3.forward, movementSpeed * -1F);
            }
        }
        else if (_movementDirection == 1)   // Left Key Up
        {
            _movementDirection = 0;
            _movementTimer = 0.0f; // Timer Reset
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_movementDirection != 2)
            {
                _movementTimer = 0.0f; // Timer Reset
                _movementDirection = 2;
            }

            if (player.transform.position.x < rightBorder.position.x && (player.transform.position.y > rightBorder.position.y || player.transform.position.x < 0F))
            {
                // rotate player
                player.transform.RotateAround(currentPlanet.position, Vector3.forward, movementSpeed * -1F);
            }
            else
            {
                // rotate planet
                allPlanets.RotateAround(currentPlanet.position, Vector3.forward, movementSpeed);
            }
        }
        else if (_movementDirection == 2)
        {
            _movementDirection = 0;
            _movementTimer = 0.0f; // Timer Reset
        }

        if (isJumpingAllowed && Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Player>().isGrounded)
        {
            _playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
        }

        _movementTimer += Time.deltaTime;

        if (_movementDirection != 0 && moveIncrDelayInSec != 0 && _movementTimer > moveIncrDelayInSec)
        {
            movementSpeed += movementSpeed * _incCounter;
            _movementTimer = 0.0F;
        }

        if (movementSpeed > 100)
        {

        }
    }
}
