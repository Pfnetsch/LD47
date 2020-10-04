using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMarsMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 0.2F;
    private float _saveInitial;

    SpriteRenderer _dustRenderer;
    Transform _dustTransform;

    private CircleCollider2D _colliderSurface;
    private Transform _marsSpriteTransform;

    private float _initialSpriteSizeStep;

    private void Start()
    {
        _colliderSurface = GetComponentInChildren<CircleCollider2D>();
        _marsSpriteTransform = this.transform.Find("MarsSprite").transform;

        _initialSpriteSizeStep = _marsSpriteTransform.localScale.x / 10;
        _saveInitial = _initialSpriteSizeStep;
    }

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 5;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        _dustRenderer = rootGameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _dustTransform = rootGameObject.transform.Find("DustAnimation");

    }

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        Debug.Log("Mars Movement");

        Vector3 scaleChange = new Vector3(_initialSpriteSizeStep / _marsSpriteTransform.localScale.x, _initialSpriteSizeStep / _marsSpriteTransform.localScale.y, 0) * Time.deltaTime;

        // move and degrade
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _dustTransform.gameObject.SetActive(true);
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 1);
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);

            _marsSpriteTransform.localScale -= scaleChange;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _dustTransform.gameObject.SetActive(true);
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 2);
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);

            _marsSpriteTransform.localScale -= scaleChange;
        }
        else
        {
            _dustTransform.gameObject.SetActive(false);
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 0);
        }

        if (_marsSpriteTransform.localScale.x < 0.5F)
        {
            _initialSpriteSizeStep = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerBody.GetComponent<Player>().isGrounded)
        {
            playerBody.AddRelativeForce(new Vector2(0.0F, 0.5F * ((_saveInitial / _marsSpriteTransform.localScale.x) * 100)), ForceMode2D.Impulse);
        }
    }
}
