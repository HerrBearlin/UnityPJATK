using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * _speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Consts.ENEMY))
        {
        }
        Destroy(gameObject);
    }
}
