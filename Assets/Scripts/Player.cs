using Bolt;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool keepRunning = false;

    // Collision Variables
    public bool isGrounded { get { return _isGrounded; } }
    public bool isUnderWater { get { return _isUnderWater; } }
    public bool isButtonPressed { get { return _isButtonPressed; } }
    public bool isAtLocation { get { return _isAtLocation; } }
    public bool isAtRocket { get { return _isAtRocket; } }
    public bool hasJetPack { get { return _hasJetpack; } }

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

    public bool IsSatelliteVisible
    {
        get => _isSatelliteVisible;
        set
        {
            _isSatelliteVisible = value;
            _satelliteSprite.gameObject.SetActive(_isSatelliteVisible);
        }
    }

    public List<AnimatorOverrideController> animationControllersMoveJump;
    public List<AnimatorOverrideController> animationControllersMoveJetpack;
    public RuntimeAnimatorController animationControllerSwim;

    private bool _isPlayerVisible = true;
    private bool _isRocketVisible = false;
    private bool _isSatelliteVisible = false;

    private bool _isGrounded = false;
    private bool _isUnderWater = false;
    private bool _isButtonPressed = false;
    private bool _isAtLocation = false;
    private bool _isAtRocket = false;
    private bool _hasJetpack = false;
    private bool _laserTransitionStarted = false;
    private bool _speechBubbleActive = false;

    private string _secondSpeechBubble;
    private float _secondSpeechBubbleDuration;
    private float _distancePlayerMovedAway = 0F;

    private Animator _animator;
    private Transform _laserTrans;
    private Transform _zopfnTrans;
    private Transform _zopfnTarget;

    private Transform _sprite;
    private Transform _rocketSprite;
    private Transform _satelliteSprite;
    private Transform _speechBubble;

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
        _satelliteSprite = transform.Find("SatelliteSprite");

        MeshRenderer blackOut = transform.Find("Blackout").GetComponent<MeshRenderer>();
        blackOut.sortingLayerName = "Default";
        blackOut.sortingOrder = 2;

        Transform uiCanvas = transform.Find("UICameraCanvas");
        _speechBubble = uiCanvas.Find("SpeechBubble");

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

    /// <summary>
    /// 0 Default Animations, 1 with Jetpack
    /// </summary>
    public void SwitchAnimations(int animIndex)
    {
        if (animIndex == 0)
        {
            _animator.runtimeAnimatorController = animationControllersMoveJump[GlobalInformation.CharacterSkinIndex];
        }
        else if (animIndex == 1)
        {
            _animator.runtimeAnimatorController = animationControllersMoveJetpack[GlobalInformation.CharacterSkinIndex];
            _hasJetpack = true;
        }
    }

    public void SatelliteGrabPlayerAndMoveToNextPlanet()
    {
        Variables.Application.Set("inTransition", true);

        _satelliteSprite.Translate(20F, 0, 0);
        IsSatelliteVisible = true;
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
            if (!_isUnderWater && !_hasJetpack) // Normal Planet Movement
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
            else if (!isUnderWater && _hasJetpack)  // Movement with Jetpack
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
                    _animator.SetInteger("Index", 2);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    _animator.SetInteger("Index", 2);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    _animator.SetInteger("Index", 3);
                }
            }
        }
        else
        {
            _animator.SetInteger("Index", 0);

            if (Variables.Application.Get<bool>("laserTransition"))
            {
                if (!_laserTransitionStarted)
                {
                    _laserTrans.gameObject.SetActive(true);
                    _laserTransitionStarted = true;
                }
                else if (_laserTransitionStarted)
                {
                    _zopfnTrans.position = Vector3.MoveTowards(_zopfnTrans.position, _zopfnTarget.position, 8F * Time.deltaTime);
                }
            }
        }

        if (IsSatelliteVisible)
        {
            if (_satelliteSprite.localPosition.x > 0.807F)
            {
                _satelliteSprite.transform.Translate(-7F * Time.deltaTime, 0, 0);
            }
            else
            {
                //transform.Translate(13F * Time.deltaTime, 0, 0);
                transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("BlackHole").transform.position, 13F * Time.deltaTime);

                _distancePlayerMovedAway += 13F * Time.deltaTime;

                if (_distancePlayerMovedAway > 50F)
                {
                    transform.Find("Blackout").gameObject.SetActive(false);

                    if (_distancePlayerMovedAway > 100F)
                    {
                        GetComponentInChildren<Camera>().orthographicSize = 20;
                    }
                }
            }

            if (isAtLocation)   // Black Hole reached
            {
                GlobalInformation.currentScene = 1;
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
        }
    }

    public void ShowSpeechBubble(string text, float durationInSec)
    {
        if (!_speechBubbleActive)
        {
            _speechBubbleActive = true;

            Transform uiCanvas = transform.Find("UICameraCanvas");
            _speechBubble = uiCanvas.Find("SpeechBubble");

            _speechBubble.gameObject.SetActive(true);
            TextMeshProUGUI textTMP = _speechBubble.GetComponentInChildren<TextMeshProUGUI>();
            textTMP.text = text;       

            StartCoroutine(WaitForSecondsBeforeClosingSpeechBubble(durationInSec));
        }
        else
        {
            _secondSpeechBubble = text;
            _secondSpeechBubbleDuration = durationInSec;
        }
    }

    private IEnumerator WaitForSecondsBeforeClosingSpeechBubble(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _speechBubbleActive = false;
        _speechBubble.gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(_secondSpeechBubble))
        {
            ShowSpeechBubble(_secondSpeechBubble, _secondSpeechBubbleDuration);
            _secondSpeechBubble = string.Empty;
        } 
    }

    public IEnumerator TransitToNextPlanet()
    {
        yield return new WaitForSeconds(2.0F);

        GlobalInformation.currentScene++;
        SceneManager.LoadScene("Transition", LoadSceneMode.Single);
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
