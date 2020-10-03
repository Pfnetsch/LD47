using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded { get { return _isGrounded; } }

    // 0 is RED
    // 1 is BLUE
    // 2 is GREEN
    // 3 is YELLOW
    public int characterSkinIndex = 0;

    //public List<Sprite>

    private bool _isGrounded = false;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
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
}
