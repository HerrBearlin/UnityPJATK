using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMeleeCombat : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;

    private Animator _animator;

    void Update()
    {
        if (Input.GetButtonDown(Constants.Consts.FIRE))
        {
            _controller.Attack(_attackPoint, _attackRange, _enemyLayer);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
