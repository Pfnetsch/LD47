using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaturnMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 5F;
    private bool _missionCleared = false;
    private float _distancePlayerMovedAway = 0;
    private bool _transitStarted = false;

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 10;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        rootGameObject.GetComponent<Player>().ShowSpeechBubble("My Jetpack is dry, \nI need to find some fuel!", 4.0F);
    }
    
    
    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        if (!_missionCleared)
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

            if (GlobalInformation.currentCollectibles >= 10)
            {
                _playerBody.GetComponent<Player>().ShowSpeechBubble("Should be enough.\nAnd i found some bright burning gas too!", 4.0F);
                _playerBody.GetComponent<Player>().SwitchAnimations(1);

                transform.Find("SaturnRing").GetComponent<EdgeCollider2D>().enabled = false;

                GlobalInformation.currentCollectibles = 0;
                _missionCleared = true;
            }
        }
        else
        {
            _distancePlayerMovedAway += _movementSpeed * 1F * Time.deltaTime;
            _playerBody.transform.Translate(0, _movementSpeed * 1F * Time.deltaTime, 0);

            if (_distancePlayerMovedAway > 3)
            {
                _playerBody.transform.Translate(_movementSpeed * 2F * Time.deltaTime, 0, 0);

                if (!_transitStarted && _distancePlayerMovedAway > 7)
                {
                    _transitStarted = true;
                    StartCoroutine(_playerBody.GetComponent<Player>().TransitToNextPlanet());                
                }
            }
        }
    }
}
