using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarsMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 0.2F;

    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        Debug.Log("Mars Movement");
        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _playerBody.GetComponent<Player>().isGrounded)
        {
            _playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
        }
    }
}
