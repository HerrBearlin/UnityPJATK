using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerScoreScriptableObject", menuName = "ScriptableObject/PlayerScore")]
public class ScoreScriptableManager : ScriptableObject
{
    public int deadScore { get; private set; }
    public int curedScore { get; private set; }

    [System.NonSerialized]
    public UnityEvent<int> deadScoreChangeEvent;
    public UnityEvent<int> curedScoreChangeEvent;

    // Update is called once per frame
    private void OnEnable()
    {
        Debug.Log("SCRIPTABLE IS ON");
        deadScore = 0;
        curedScore = 0;
        if (deadScoreChangeEvent == null)
        {
            deadScoreChangeEvent = new UnityEvent<int>();
        }
        if (curedScoreChangeEvent == null)
        {
            curedScoreChangeEvent = new UnityEvent<int>();
        }
    }

    public void IncreaseDeadScore(int amount)
    {
        deadScore += amount;
        deadScoreChangeEvent.Invoke(deadScore);
    }

    public void IncreaseCuredScore(int amount)
    {
        curedScore += amount;
        curedScoreChangeEvent.Invoke(curedScore);
    }
}