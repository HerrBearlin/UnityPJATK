using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Script : StateMachineBehaviour
{
   


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Spooky_Player._playerHealth = Spooky_Player._maxHealth;
    }

   
}
