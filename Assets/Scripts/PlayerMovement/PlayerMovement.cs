using Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public Transform allPlanets;

    private Rigidbody2D _playerBody;

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = player.GetComponent<Rigidbody2D>();
        allPlanets.GetChild(GlobalInformation.currentScene-1).GetComponent<IPlayerPlanetMovement>().PlayerSetup(player);
    }

    // Update is called once per frame
    void Update()
    {
        allPlanets.GetChild(GlobalInformation.currentScene-1).GetComponent<IPlayerPlanetMovement>().PlayerUpdate(_playerBody);
    }
}
