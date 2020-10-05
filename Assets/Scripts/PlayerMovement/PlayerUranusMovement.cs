using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUranusMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 5F;

    private float _activeTime = 0F;
    private Vector3 _probePosition;

    private Transform _directionSigns;
    private int _directionSignIndex = 0;

    private bool _probeFound = false;

    public void PlayerSetup(GameObject rootGameObject)
    {
        GlobalInformation.currentCollectibles = 0;

        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 10;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        rootGameObject.GetComponent<Player>().SwitchAnimations(1);
        rootGameObject.transform.Find("Blackout").gameObject.SetActive(true);

        _directionSigns = transform.Find("DirectionSigns");

        rootGameObject.GetComponent<Player>().ShowSpeechBubble("Huh, where am I? Quite dark here ...", 5.0F);
    }
    
    public void TurnDirectionSignToProbe()
    {
        Transform sign = _directionSigns.GetChild(_directionSignIndex);

        Vector3 diff = _probePosition - sign.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        sign.rotation = Quaternion.Euler(0F, 0F, rot_z - 90);

        sign.gameObject.SetActive(true);

        _directionSignIndex++;
    }
    
    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        if (!_probeFound)
        {
            _activeTime += Time.deltaTime;

            // The Player gets a hint all few seconds
            if (_directionSignIndex == 0 && _activeTime > 2.5F)
            {
                _probePosition = GameObject.FindGameObjectWithTag("Collectible").transform.position;
                TurnDirectionSignToProbe();
            }
            else if (_directionSignIndex == 1 && _activeTime > 5.0F)
                TurnDirectionSignToProbe();
            else if (_directionSignIndex == 2 && _activeTime > 10.0F)
                TurnDirectionSignToProbe();
            else if (_directionSignIndex == 3 && _activeTime > 15.0F)
                TurnDirectionSignToProbe();
            else if (_directionSignIndex == 4 && _activeTime > 20.0F)
                TurnDirectionSignToProbe();
            else if (_directionSignIndex == 5 && _activeTime > 25.0F)
                TurnDirectionSignToProbe();

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _playerBody.transform.Translate(0, _movementSpeed * 1F * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _playerBody.transform.Translate(0, _movementSpeed * -1F * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _playerBody.transform.Translate(_movementSpeed * -1F * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _playerBody.transform.Translate(_movementSpeed * 1F * Time.deltaTime, 0, 0);
            }

            //// move left
            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    _playerBody.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * 0.03F * Time.deltaTime * 360 / (Vector3.Distance(_playerBody.position, gameObject.transform.position)) * 2 * (float)Math.PI);
            //}

            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    _playerBody.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -0.03F * Time.deltaTime * 360 / (Vector3.Distance(_playerBody.position, gameObject.transform.position)) * 2 * (float)Math.PI);
            //}

            if (GlobalInformation.currentCollectibles >= 1)
            {
                _playerBody.GetComponent<Player>().ShowSpeechBubble("Maybe i can use this to get a lift!", 5.0F);
                _playerBody.GetComponent<Player>().SatelliteGrabPlayerAndMoveToNextPlanet();
                transform.Find("UranusSprite").GetComponent<EdgeCollider2D>().enabled = false;
                _probeFound = true;
            }
        }
    }
}
