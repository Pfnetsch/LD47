using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;

    public Transform planet;
    public Transform leftBorder;
    public Transform rightBorder;

    public float movementSpeed = 0.5F;
    public bool isJumpingAllowed = false;

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
            if (player.transform.position.x > leftBorder.position.x && (player.transform.position.y > leftBorder.position.y || player.transform.position.x > 0F))
            {
                // rotate player
                player.transform.RotateAround(planet.position, Vector3.forward, movementSpeed);
            }
            else
            {
                // rotate planet
                planet.RotateAround(planet.position, Vector3.forward, movementSpeed * -1F);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (player.transform.position.x < rightBorder.position.x && (player.transform.position.y > rightBorder.position.y || player.transform.position.x < 0F))
            {
                // rotate player
                player.transform.RotateAround(planet.position, Vector3.forward, movementSpeed * -1F);
            }
            else
            {
                // rotate planet
                planet.RotateAround(planet.position, Vector3.forward, movementSpeed);
            }
        }

        if (isJumpingAllowed && Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Player>().isGrounded)
        {
            _playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
        }
    }
}
