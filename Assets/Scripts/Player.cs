using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool keepRunning = false;

    // Collision Variables
    public bool isGrounded { get { return _isGrounded; } }
    public bool isUnderWater { get { return _isUnderWater; } }
    public bool isButtonPressed { get { return _isButtonPressed; } }
    public bool isAtLocation { get { return _isAtLocation; } }
    public bool isAtRocket { get { return _isAtRocket; } }

    public bool IsPlayerVisible
    { 
        get => _isPlayerVisible;
        set
        {
            _isPlayerVisible = value;
            _sprite.gameObject.SetActive(_isPlayerVisible);
        }
    }

    public bool IsRocketVisible
    { 
        get => _isRocketVisible;
        set
        {
            _isRocketVisible = value;
            _rocketSprite.gameObject.SetActive(_isRocketVisible);
        }
    }

    public List<AnimatorOverrideController> animationControllersMoveJump;
    public UnityEditor.Animations.AnimatorController animationControllerSwim;

    private bool _isPlayerVisible = true;
    private bool _isRocketVisible = false;

    private bool _isGrounded = false;
    private bool _isUnderWater = false;
    private bool _isButtonPressed = false;
    private bool _isAtLocation = false;
    private bool _isAtRocket = false;
    private bool _lasterTransitionStarted = false;

    private Animator _animator;
    private Transform _laserTrans;
    private Transform _zopfnTrans;
    private Transform _zopfnTarget;

    private Transform _sprite;
    private Transform _rocketSprite;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.runtimeAnimatorController = animationControllersMoveJump[GlobalInformation.CharacterSkinIndex];

        _laserTrans = transform.Find("LaserTransition");
        _zopfnTrans = _laserTrans.Find("Zopfn");
        _zopfnTarget = _laserTrans.Find("ZopfnTarget");

        _sprite = transform.Find("Sprite");
        _rocketSprite = transform.Find("RocketSprite");

        if (Variables.Application.Get<bool>("laserTransition"))
        {
            IsPlayerVisible = false;
        }

        if (Variables.Application.Get<bool>("inRocket"))
        {
            IsPlayerVisible = false;
            IsRocketVisible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Stop Movement if in Transition
        if (!Variables.Application.Get<bool>("inTransition"))
        {
            if (keepRunning)
            {
                _animator.SetInteger("Index", 2);
                return;
            }
            if (!_isUnderWater)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    _animator.SetInteger("Index", 1);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    _animator.SetInteger("Index", 2);
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    _animator.SetInteger("Index", 3);
                }
                else
                {
                    _animator.SetInteger("Index", 0);
                }
            }
        }
        else
        {
            _animator.SetInteger("Index", 0);

            if (Variables.Application.Get<bool>("laserTransition"))
            {
                if (!_lasterTransitionStarted)
                {
                    _laserTrans.gameObject.SetActive(true);
                    _lasterTransitionStarted = true;
                }
                else if (_lasterTransitionStarted)
                {
                    _zopfnTrans.position = Vector3.MoveTowards(_zopfnTrans.position, _zopfnTarget.position, 8F * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterSurface"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            _isUnderWater = true;
            _animator.runtimeAnimatorController = animationControllerSwim;
            _animator.SetInteger("CharIndex", GlobalInformation.CharacterSkinIndex);
        }
        if (collision.CompareTag("Button"))
        {
            _isButtonPressed = true;
        }
        if (collision.CompareTag("Location"))
        {
            _isAtLocation = true;
        }
        if (collision.CompareTag("Rocket"))
        {
            _isAtRocket = true;
        }
        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            GlobalInformation.currentCollectibles++;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterSurface"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            _isUnderWater = false;
            _animator.runtimeAnimatorController = animationControllersMoveJump[GlobalInformation.CharacterSkinIndex];
        }
        if (collision.CompareTag("Button"))
        {
            _isButtonPressed = false;
        }
        if (collision.CompareTag("Location"))
        {
            _isAtLocation = false;
        }
        if (collision.CompareTag("Rocket"))
        {
            _isAtRocket = false;
        }
    }
}
