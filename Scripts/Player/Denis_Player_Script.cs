using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denis_Player_Script : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _movementMultiplier;
    private float _xDisplacement;
    private float _yDisplacement;
    private Animator _animator;
    /*[Range(0, .3f)][SerializeField] private float _movementSmoothing = .05f;
    private float _raycastLength = 0.6f;
    private Vector3 _velocity = Vector3.zero;*/

    void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _xDisplacement = Input.GetAxis(Constants.Consts.HORIZONTAL);
        _yDisplacement = Input.GetAxis(Constants.Consts.VERTICAL);

        _animator.SetBool("IsWalking", _xDisplacement != 0 || _yDisplacement != 0);
    }
    void FixedUpdate()
    {
        _controller.Move(_xDisplacement * _movementSpeed * _movementMultiplier,
                         _yDisplacement * _movementSpeed * _movementMultiplier);
    }
    /*private void Jump()
    {
        //_rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }*/
    /*private bool IsGrounded()
    {
        var raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, _raycastLength, _platformsLayerMask);
        return raycastHit2D.collider != null;
    }*/
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _raycastLength);
    }*/
}
