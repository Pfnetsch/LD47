using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMarsMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 0.2F;

    private CircleCollider2D _colliderSurface;
    private Transform _marsSpriteTransform;

    private float _initialSpriteSizeStep;

    private void Start()
    {
        _colliderSurface = GetComponentInChildren<CircleCollider2D>();
        _marsSpriteTransform = this.transform.Find("MarsSprite").transform;

        _initialSpriteSizeStep = _marsSpriteTransform.localScale.x / 10;
    }

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 5;
    }

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        Debug.Log("Mars Movement");

        Vector3 scaleChange = new Vector3(_initialSpriteSizeStep / _marsSpriteTransform.localScale.x, _initialSpriteSizeStep / _marsSpriteTransform.localScale.y, 0) * Time.deltaTime;

        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);
            
            _marsSpriteTransform.localScale -= scaleChange;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);

            _marsSpriteTransform.localScale -= scaleChange;
        }

        if (_marsSpriteTransform.localScale.x < 0.5F)
        {
            _initialSpriteSizeStep = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerBody.GetComponent<Player>().isGrounded)
        {
            playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
        }
    }
}
