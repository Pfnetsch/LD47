using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUranusMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 5F;

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 10;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        rootGameObject.transform.Find("Blackout").gameObject.SetActive(true);
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
            _playerBody.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * 0.03F * Time.deltaTime * 360/ (Vector3.Distance(_playerBody.position, gameObject.transform.position)) * 2 * (float)Math.PI);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _playerBody.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -0.03F * Time.deltaTime * 360/ (Vector3.Distance(_playerBody.position, gameObject.transform.position)) * 2 * (float)Math.PI);
        }
    }
}
