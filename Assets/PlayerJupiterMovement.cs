using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJupiterMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 4F;

    private Transform _sideScroller;
    private Transform _platforms;

    // Start is called before the first frame update
    void Start()
    {
        _sideScroller = transform.Find("Spawn").transform.Find("SideScrollerBox");
        _platforms = transform.Find("Spawn").transform.Find("Platforms");

        Transform firstPlatforms = _platforms.Find("1");

        Instantiate(firstPlatforms, firstPlatforms.position + new Vector3(72F, 0, 0), Quaternion.identity, _platforms).name = "2";
        Instantiate(firstPlatforms, firstPlatforms.position + new Vector3(144F, 0, 0), Quaternion.identity, _platforms).name = "3";
        Instantiate(firstPlatforms, firstPlatforms.position + new Vector3(216F, 0, 0), Quaternion.identity, _platforms).name = "4";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 10;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0F;
        rootGameObject.GetComponent<Player>().SwitchAnimations(1);
    }

    private float _lengthPlatformsMoved = 15; // Inital Space
    private int _indexPlatFormToReset = 0;

    public void PlayerUpdate(Rigidbody2D _playerBody)
    {
        float moveDiff = _movementSpeed * -1F * Time.deltaTime;

        _lengthPlatformsMoved += moveDiff;

        _playerBody.transform.Translate(-moveDiff, 0, 0);
        _platforms.Translate(moveDiff * 2, 0, 0);   // Platforms are faster

        if (_lengthPlatformsMoved < -142F)
        {
            _platforms.GetChild(_indexPlatFormToReset).Translate(288F, 0, 0);
            _indexPlatFormToReset = ++_indexPlatFormToReset % 4;
            _lengthPlatformsMoved = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _playerBody.transform.Translate(0, _movementSpeed * 1F * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _playerBody.transform.Translate(0, _movementSpeed * -1F * Time.deltaTime, 0);
        }
     
        //// Left / Right Movement is slower for Balancing
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    moveDiff = _movementSpeed * -0.5F * Time.deltaTime;
        //    _playerBody.transform.Translate(moveDiff, 0, 0);
        //    _lengthPlatformsMoved -= moveDiff;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    moveDiff = _movementSpeed * 0.5F * Time.deltaTime;
        //    _playerBody.transform.Translate(moveDiff, 0, 0);
        //    _lengthPlatformsMoved -= moveDiff;
        //}
    }
}
