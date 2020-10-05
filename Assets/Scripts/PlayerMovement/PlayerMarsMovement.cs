using Bolt;
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
        Variables.Application.Set("inRocket", false);

        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 5;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        _dustRenderer = rootGameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _dustTransform = rootGameObject.transform.Find("DustAnimation");
        _dustTransform.gameObject.SetActive(true);
    }

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        //Debug.Log("Mars Movement");
        Vector3 scaleChange = new Vector3(_initialSpriteSizeStep / _marsSpriteTransform.localScale.x, _initialSpriteSizeStep / _marsSpriteTransform.localScale.y, 0) * Time.deltaTime;

        // move and degrade
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 1);
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);

            _marsSpriteTransform.localScale -= scaleChange;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 2);
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);

            _marsSpriteTransform.localScale -= scaleChange;
        }
        else
        {
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 0); // No dust Animation
        }

        if (_initialSpriteSizeStep == 0 && Input.GetKeyDown(KeyCode.Space))
        {

            playerBody.AddRelativeForce(new Vector2(0.0F, 2F), ForceMode2D.Force);
        }

        if (_marsSpriteTransform.localScale.x < 0.5F)
        {
            _initialSpriteSizeStep = 0;
            playerBody.GetComponent<Player>().ShowSpeechBubble("Hm.. The gravity is low already. \nLet's try the jetpack :)");
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerBody.GetComponent<Player>().isGrounded)
        {
            playerBody.AddRelativeForce(new Vector2(0.0F, 0.5F * ((_saveInitial / _marsSpriteTransform.localScale.x) * 100)), ForceMode2D.Impulse);
        }
    }
}
