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
    private bool _transitStarted = false;

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

        rootGameObject.GetComponent<Player>().ShowSpeechBubble("Oh no i made a crash landing on mars.\nMaybe I should have used more duct tape ...", 5.0F);
        rootGameObject.GetComponent<Player>().ShowSpeechBubble("Only a pair of ancient \"highheels\" and\na jetpack survived the landing.\nI should definitly test those fancy shoes first!", 5.0F);
    }

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        //Debug.Log("Mars Movement");

        // move and degrade
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 scaleChange = new Vector3(_initialSpriteSizeStep / _marsSpriteTransform.localScale.x, _initialSpriteSizeStep / _marsSpriteTransform.localScale.y, 0) * Time.deltaTime;

            if (_initialSpriteSizeStep != 0)
            {
                _dustTransform.GetComponent<Animator>().SetInteger("Index", 1);
                _marsSpriteTransform.localScale -= scaleChange;
            }

            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 scaleChange = new Vector3(_initialSpriteSizeStep / _marsSpriteTransform.localScale.x, _initialSpriteSizeStep / _marsSpriteTransform.localScale.y, 0) * Time.deltaTime;

            if (_initialSpriteSizeStep != 0)
            {
                _dustTransform.GetComponent<Animator>().SetInteger("Index", 2);
                _marsSpriteTransform.localScale -= scaleChange;
            }

            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);
        }
        else
        {
            _dustTransform.GetComponent<Animator>().SetInteger("Index", 0); // No dust Animation
        }

        if (!_transitStarted && _initialSpriteSizeStep == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            playerBody.AddRelativeForce(new Vector2(0.0F, 2F), ForceMode2D.Force);

            _transitStarted = true;
            StartCoroutine(playerBody.GetComponent<Player>().TransitToNextPlanet());
        }

        if (_initialSpriteSizeStep != 0 && _marsSpriteTransform.localScale.x < 0.5F)
        {
            _initialSpriteSizeStep = 0;
            playerBody.GetComponent<Player>().ShowSpeechBubble("Hm, that's unfortunate, I liked Mars.\nBut now the gravity should be low enought to use the jetpack!", 10.0F);
            playerBody.GetComponent<Player>().SwitchAnimations(1);  // Switch to Jetpack
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerBody.GetComponent<Player>().isGrounded)
        {
            playerBody.AddRelativeForce(new Vector2(0.0F, 0.5F * ((_saveInitial / _marsSpriteTransform.localScale.x) * 100)), ForceMode2D.Impulse);
        }
    }
}
