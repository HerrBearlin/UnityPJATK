using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action<bool> OnMutantMovement;
    public static event Action<bool> OnMutantAttack;

    public static event Action<float> OnPlayerHealthChange;
    public static event Action<float> OnPlayerRageChange;


    public static void MutantMovement(bool inRange)
    {
        OnMutantMovement?.Invoke(inRange);
    }

    public static void MutantAttack(bool isAttack)
    {
        OnMutantAttack?.Invoke(isAttack);
    }

    public static void PlayerHealthChange(float health)
    {
        OnPlayerHealthChange?.Invoke(health);
    }

    public static void PlayerRageChange(float rage)
    {
        OnPlayerRageChange?.Invoke(rage);
    }
}
