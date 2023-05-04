using Constants;
using System.Collections;
using UnityEngine;
using Pathfinder;
using Unity.VisualScripting;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _player;
        [SerializeField] private float _speed;
        private SpriteRenderer _sr;
        private Vector2[] path;
        private int index;


        private void Start()
        {
            GameObject _game = GameObject.FindGameObjectWithTag(Consts.PLAYER);
            _player = _game.transform;
            _sr = GetComponent<SpriteRenderer>();

            EventManager.MutantMovement(true);
            StartCoroutine(updatePath());
        }

        private void Update()
        {
            
        }
        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Consts.PLAYER))
            {
                Debug.Log("FOUND PLAYER");
                EventManager.MutantMovement(true);
                StartCoroutine(updatePath());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Consts.PLAYER))
            {
                Debug.Log("MUST STOP FOLLOWING PLAYER");
                EventManager.MutantMovement(false);
                StopAllCoroutines();
                //StopCoroutine(updatePath());
                //StopCoroutine("moveToPlayer");
            }
        }*/

        private void Flip(bool onRight)
        {
            if (onRight)
            {
                _sr.flipX = false;
            }
            else if (!onRight)
            {
                _sr.flipX = true;
            }
        }

        private IEnumerator updatePath()
        {
            Vector2 targetPositionOld = Vector2.zero;

            while (true)
            {
                path = Pathfinder.Pathfinder.RequestPath(transform.position, _player.position);
                Flip(transform.position.x < _player.position.x);
                StopCoroutine("moveToPlayer");
                StartCoroutine("moveToPlayer");

                yield return new WaitForSeconds(.25f);
            }
        }

        private IEnumerator moveToPlayer()
        {
            if (path.Length > 0)
            {
                index = 0;
                Vector2 nextPosition = path[0];

                while (true)
                {
                    if ((Vector2)transform.position == nextPosition)
                    {
                        index++;
                        if (index >= path.Length)
                        {
                            yield break;
                        }
                        nextPosition = path[index];
                    }
                    transform.position = Vector2.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                yield break;
            }
        }

        private void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = index; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;

                    if (i == index)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, 20f);
        }
    }
}
