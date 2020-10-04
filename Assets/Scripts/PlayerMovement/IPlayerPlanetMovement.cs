using UnityEngine;

public interface IPlayerPlanetMovement
{
    void PlayerSetup(GameObject rootGameObject);
    void PlayerUpdate(Rigidbody2D _playerBody);
}