using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firepoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _trancBulletPrefab;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Transform _player;
    private float _cooldownTimer = Mathf.Infinity;
    private Camera _mainCam;
    private Vector3 _mousePos;
    private WeaponSwitcher _ws;
    private bool _isFacingRight = true;

    private void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag(Constants.Consts.MAINCAMERA).GetComponent<Camera>();
        _ws = gameObject.GetComponentInChildren<WeaponSwitcher>();
    }

    void Update()
    {
        _cooldownTimer += Time.deltaTime;
        //Debug.Log(_cooldownTimer);
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        var rotation = _mousePos - transform.position;

        var pRotation = _mousePos - _player.transform.position;
        var rotY = pRotation.x;

        if(rotY > 0 && !_isFacingRight)
        {

            Flip();
        }
        if (rotY < 0 && _isFacingRight)
        {
            Flip();
        }

        float rotZ = Mathf.Atan2(rotation.y , rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (_cooldownTimer >= _attackCooldown)
        {
            if (Input.GetButton(Constants.Consts.FIRE))
            {
                _cooldownTimer = 0;
                Shoot();
            }
        }
        
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        _player.transform.Rotate(0f, 180f, 0f);
    }

    private void Shoot()
    {
        if(_ws._currentWeapon == 0)
            Instantiate(_bulletPrefab, _firepoint.position, _firepoint.rotation);
        else
            Instantiate(_trancBulletPrefab, _firepoint.position, _firepoint.rotation);

    }
}
