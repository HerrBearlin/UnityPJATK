using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private ScoreScriptableManager _scoreManager;
    [SerializeField] private TMP_Text _DeadScoreLabel;
    [SerializeField] private TMP_Text _CuredScoreLabel;
    

    private void OnEnable()
    {
        _scoreManager.deadScoreChangeEvent.AddListener(SetDeadScoreLabel);
        _scoreManager.curedScoreChangeEvent.AddListener(SetCuredScoreLabel);
    }

    private void OnDisable()
    {
        _scoreManager.deadScoreChangeEvent.RemoveListener(SetDeadScoreLabel);
        _scoreManager.curedScoreChangeEvent.RemoveListener(SetCuredScoreLabel);
    }

    private void Start()
    {

    }

    private void SetDeadScoreLabel(int number)
    {
        _DeadScoreLabel.text = $": {number}";
    }

    private void SetCuredScoreLabel(int number)
    {
        _CuredScoreLabel.text = $": {number}";
    }
}
