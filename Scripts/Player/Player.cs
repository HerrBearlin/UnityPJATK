using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _rage;
    //[SerializeField] private ScoreScriptableManager _scoreManager;


    private void Start()
    {
        EventManager.OnPlayerHealthChange += ChangeHealth;
        EventManager.OnPlayerRageChange += ChangeRage;
    }

    /*private void OnEnable()
    {
        _scoreManager.deadScoreChangeEvent.AddListener(printDeadScore);
        _scoreManager.curedScoreChangeEvent.AddListener(printCuredScore);
    }

    private void OnDisable()
    {
        _scoreManager.deadScoreChangeEvent.RemoveListener(printDeadScore);
        _scoreManager.curedScoreChangeEvent.RemoveListener(printCuredScore);
    }*/


    private void Update()
    {
        
    }

    /*private void printDeadScore(int amount)
    {
        Debug.Log("DEAD SCORE: " + amount);
    }

    private void printCuredScore(int amount)
    {
        Debug.Log("CURED SCORE: " + amount);
    }*/

    private void ChangeHealth(float health)
    {
        _health += health;
        Debug.Log("PLAYER.HEALTH " + _health);
    }

    private void ChangeRage(float rage)
    {
        _rage += rage;
        Debug.Log("PLAYER.RAGE " + _rage);
    }


    private void OnDestroy()
    {
        EventManager.OnPlayerHealthChange -= ChangeHealth;
        EventManager.OnPlayerRageChange -= ChangeRage;
    }
}
