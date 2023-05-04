using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public int _currentWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int prevSelectedWeapon = _currentWeapon;
        if (Input.GetButtonDown(Constants.Consts.SWITCHWEAPON))
        {
            if (_currentWeapon >= transform.childCount -1)
                _currentWeapon = 0;
            else
                _currentWeapon++;
        }
        if(prevSelectedWeapon != _currentWeapon)
        {
            SelectWeapon();
        }
    }
    private void SelectWeapon()
    {
        var i = 0;
        //Debug.Log(_currentWeapon);
        foreach (Transform weapon in transform)
        {
            if (i == _currentWeapon)
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
