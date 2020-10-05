using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJupiterMovement : MonoBehaviour, IPlayerPlanetMovement
{
    // Start is called before the first frame update
    void Start()
    {

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

    public void PlayerUpdate(Rigidbody2D _playerBody)
    {

    }
}
