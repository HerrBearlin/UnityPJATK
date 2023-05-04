using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

//This Script is intended for demoing and testing animations only.


public class Spooky_Player : MonoBehaviour {

	
	//private float maxVertHSpeed = 20f;
	private bool facingRight = true;
	private float moveXInput;


	[SerializeField] private float _iAmSpeed;
	[SerializeField] private float _iAmJump;
	[SerializeField] private float _iAmDash;


    [SerializeField] private Rigidbody2D _rB;

    private bool isDedge = false;

    private float spaceTapSpeed = 0.5f;
	private float lastTap = 0f;
	private bool isOnGround;
    [SerializeField] public static float _maxHealth = 3f;
    [SerializeField] public static float _playerHealth = 3f;

    //Used for flipping Character Direction
    public static Vector3 theScale;

	public Transform groundCheck;
	public LayerMask whatIsGround;
	

	private Animator anim;


	void Awake ()
    { 
        _rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator> ();
	}


	void Update()
	{

    

        float moveXInput = Input.GetAxis(Axis.HORIZONTAL);
        Vector2 displacementV = new Vector2(moveXInput, 0);

        transform.Translate(displacementV * Time.deltaTime * _iAmSpeed);

        if (Input.GetButtonDown(Axis.JUMP) && isOnGround)
        {

            Debug.Log("I am jumping");
            anim.SetBool("ground", false);
            _rB.AddForce(new Vector2(0, _iAmJump), ForceMode2D.Impulse);

            lastTap = Time.time;
        }
        else if (Input.GetButtonDown(Axis.JUMP) && (Time.time - lastTap) < spaceTapSpeed)
        {
            /* Debug.Log("I am double jumping");*/
            _rB.AddForce(new Vector2(0, _iAmJump), ForceMode2D.Impulse);
            lastTap = Time.time;
        }
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetButton(Axis.SPRINT) && isOnGround)
        {
            Debug.Log("I am sprinting.");
            transform.Translate(displacementV * (_iAmSpeed * 2) * Time.deltaTime);
            anim.SetBool("Sprint", true);
            
        }
        else
        {
            anim.SetBool("Sprint", false);
        }
        anim.SetFloat("HSpeed", Mathf.Abs(moveXInput));

       

        //Flipping direction character is facing based on players Input
        if (moveXInput > 0 && !facingRight)
            Flip();
        else if (moveXInput < 0 && facingRight)
            Flip();

        
        //PLAYER DYING
        if (_playerHealth <= 0 && !isDedge)
        {
            PlayerDie();
            isDedge = true;
            
        }
        
    }
    ////Flipping direction of character
    void Flip()
	{
		facingRight = !facingRight;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    private void PlayerDie()
    {
        Debug.Log("Player died.");
        anim.SetTrigger("Dead");
        anim.SetBool("IsDead", true);
       
    }


    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.GROUND))
        {
            isOnGround = true;
            anim.SetBool("ground", true);
        }

        if(collision.gameObject.CompareTag(Tag.ENEMY))
        {
            yield return new WaitForSeconds(1);
            _playerHealth--;
            Debug.Log("Taking damage");
            Debug.Log(_playerHealth);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.GROUND))
        {
            anim.SetBool("ground", false);
            isOnGround = false;
        }

    }

    

}
