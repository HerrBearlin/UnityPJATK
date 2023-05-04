using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float _jumpForce;
    //[SerializeField] private LayerMask _platformsLayerMask;
    private Rigidbody2D _rb;
    
    //private bool _isFacingRight = true;
    private Transform _meleeCombat;
    private Transform _rangeCombat;
    //private Animator _animator;
    /*[Range(0, .3f)][SerializeField] private float _movementSmoothing = .05f;
    private float _raycastLength = 0.6f;
    private Vector3 _velocity = Vector3.zero;*/
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();

        _meleeCombat = transform.Find(Constants.Consts.MELEECOMBAT);
        _rangeCombat = transform.Find(Constants.Consts.RANGECOMBAT);
        _meleeCombat.gameObject.SetActive(false);
        _rangeCombat.gameObject.SetActive(true);
        //Debug.Log(_meleeCombat.name + " " + _rangeCombat.name);
    }

    void Update()
    {
        
    }

    public void Move(float xDisplacement, float yDisplacement)
    {
        
        //Vector3 xDisplacementVector = new Vector3(xDisplacement, 0, 0);
        _rb.velocity = new Vector2(xDisplacement * Time.deltaTime, _rb.velocity.y);
        //transform.Translate(xDisplacementVector * _movementSpeed * Time.deltaTime);

        
        _rb.velocity = new Vector2(_rb.velocity.x, yDisplacement * Time.deltaTime);
        //Vector3 yDisplacementVector = new Vector3(0, yDisplacement, 0);
        //transform.Translate(yDisplacementVector * _movementSpeed * Time.deltaTime);

        /*if (xDisplacement > 0.01f && !_isFacingRight)
            Flip();
        else if (xDisplacement < -0.01f && _isFacingRight)
            Flip();*/

        /*if (IsGrounded() && Input.GetButton(Constants.Consts.JUMP))
        {
            Jump();
        }*/
    }
    /*private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }*/

    public void Attack(Transform attackPoint, float attackRange, LayerMask enemyLayer)
    {
        if(_meleeCombat.gameObject.activeInHierarchy == true)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            Debug.Log(attackPoint.position + " " + attackRange);
            foreach (var enemy in hitEnemies)
            {
                if (Vector2.Distance(attackPoint.position, enemy.gameObject.transform.position) < attackRange)
                    Destroy(enemy.gameObject);

            }
        }
    }
}
