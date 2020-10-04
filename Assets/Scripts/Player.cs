using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded { get { return _isGrounded; } }
    public bool isUnderWateer { get { return _isUnderWater; } }

    public List<AnimatorOverrideController> animationControllers;

    //public List<Sprite>

    private bool _isGrounded = false;
    private bool _isUnderWater = false;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.runtimeAnimatorController = animationControllers[GlobalInformation.CharacterSkinIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
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
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterSurface"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            _isUnderWater = false;
        }
    }
}
