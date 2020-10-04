using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaturnMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 5F;

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 10;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }
    
    
    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _playerBody.transform.Translate(0, _movementSpeed * 1F * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _playerBody.transform.Translate(0, _movementSpeed * -1F * Time.deltaTime, 0);
        }
        
        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _playerBody.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * 1F * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _playerBody.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F * Time.deltaTime);
        }
    }
}
