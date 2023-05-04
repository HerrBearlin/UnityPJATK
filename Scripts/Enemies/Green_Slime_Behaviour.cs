using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Slime_Behaviour : StateMachineBehaviour
{
    private Vector2 dir = Vector2.left;
    [SerializeField] private float _speed;


    private Rigidbody2D rb;
    public Transform player;
    private bool facingRight = true;
    public static Vector3 theScale;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         

        rb.transform.Translate(dir * _speed * Time.deltaTime);
        if(Vector2.Distance(rb.position, player.position) < 4)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, _speed * Time.deltaTime);
            rb.MovePosition(newPos);
            if (Vector2.Distance(rb.position, player.position) < 2)
            {
                animator.SetTrigger("Attack");
            }
        }
        else if (rb.position.x <= Camera.main.transform.position.x - 2)
        {
            dir = Vector2.right;
        }
        else if (rb.position.x >= -Camera.main.transform.position.x + 2)
        {
            dir = Vector2.left;
        }


        if (dir == Vector2.left && facingRight)
        { 
            Flip(); 
        }
           
        else if (dir == Vector2.right && !facingRight)
        {
            Flip();
        }
            


        void Flip()
        {
            facingRight = !facingRight;
            theScale = rb.transform.localScale;
            theScale.x *= -1;
            rb.transform.localScale = theScale;
        }
    }
}


