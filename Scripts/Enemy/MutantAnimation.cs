using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


namespace Enemy
{
    public class MutantAnimation : MonoBehaviour
    {
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EventManager.OnMutantMovement += Move;
            EventManager.OnMutantAttack += Attack;
        }
        private void Move(bool inRange)
        {
            _animator.SetBool(Consts.InRANGE, inRange);
        }

        private void Attack(bool inRange)
        {
            _animator.SetBool(Consts.IsATTACK, inRange);
        }

        private void OnDestroy()
        {
            EventManager.OnMutantMovement -= Move;
            EventManager.OnMutantAttack -= Attack;
        }
    }
}

