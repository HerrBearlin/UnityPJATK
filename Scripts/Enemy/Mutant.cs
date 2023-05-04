using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Mutant : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private float _medicine;

        [SerializeField] private float damage;
        [SerializeField] private float giveRage;

        [SerializeField] private float damageTaken;
        [SerializeField] private float medicineTaken;
        [SerializeField] private ScoreScriptableManager _scoreManager;

        private Animator _animator;
        [SerializeField] private Collider2D _collider2D;


        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {

        }

        private void receiveDamage(float damage)
        {
            _health -= damage;            
            //Debug.Log("Enemy's received damage and its health =" + _health);
            if (_health <= 0)
            {
                StopAllCoroutines();
                _collider2D.enabled = false;
                _animator.SetBool(Consts.IsDEAD, true);
                _scoreManager.IncreaseDeadScore(1);
                StartCoroutine(HealingFinished(0.5f));
            }
        }

        private void receieveMedicine(float med)
        {
            _medicine += med;
            Debug.Log("Enemy's received medicine =" + _medicine);
            if (_medicine >= 100)
            {
                //Debug.Log("Enemy should be healed");
                StopAllCoroutines();
                _collider2D.enabled = false;
                _animator.SetBool(Consts.IsHEALED, true);
                _scoreManager.IncreaseCuredScore(1);
                StartCoroutine(HealingFinished(0.6f));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Consts.PLAYER))
            {
                Debug.Log("MUTANT ATTACKS");
                //EventManager.MutantAttack(true);
                _animator.SetBool(Consts.IsATTACK, true);
                EventManager.PlayerHealthChange(-damage);
                EventManager.PlayerRageChange(giveRage);
            }
            if (collision.gameObject.CompareTag(Consts.BULLET))
            {
                receiveDamage(damageTaken);
            }
            else if (collision.gameObject.CompareTag(Consts.HEALBULLET))
            {
                receieveMedicine(medicineTaken);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Consts.PLAYER))
            {
                //EventManager.MutantAttack(false);
                _animator.SetBool(Consts.IsATTACK, false);
            }
        }

        private IEnumerator HealingFinished(float _time)
        {
            yield return new WaitForSeconds(_time);
            Destroy(gameObject);
        }
    }
}