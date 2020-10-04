using Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public Transform allPlanets;

    // Increasing Speed - Mercury
    private int _movementDirection = 0; // 0 standing still, 1 is left, 2 is right


    private Rigidbody2D _playerBody;

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        allPlanets.GetChild(GlobalInformation.currentScene-1).GetComponent<IPlayerPlanetMovement>().PlayerUpdate(_playerBody);
    }
}
