using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool keepRunning = false;
    public bool isGrounded { get { return _isGrounded; } }
    public bool isUnderWater { get { return _isUnderWater; } }

    public bool isButtonPressed { get { return _isButtonPressed; } }

    public List<AnimatorOverrideController> animationControllersMoveJump;
    public UnityEditor.Animations.AnimatorController animationControllerSwim;

    //public List<Sprite>

    private bool _isGrounded = false;
    private bool _isUnderWater = false;
    private bool _isButtonPressed = false;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.runtimeAnimatorController = animationControllersMoveJump[GlobalInformation.CharacterSkinIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
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

        if (Variables.Application.Get<bool>("laserTransition"))
        {
            transform.Find("Sprite").gameObject.SetActive(false);
            transform.Find("LaserTransition").gameObject.SetActive(true);
            Transform zopfnTrans = transform.Find("Zopfn");

            zopfnTrans.position = Vector3.MoveTowards(zopfnTrans.position, transform.Find("ZopfnTarget").position, 0.2F * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        else if (other.collider.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            GlobalInformation.saturnScore++;
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
    }
}
