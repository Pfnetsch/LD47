using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetMovement : MonoBehaviour
{
    public Rigidbody2D _playerBody;

    public void Jump()
    {
        _playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
    }
}
