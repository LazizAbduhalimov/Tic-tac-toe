using System;
using Game;
using TMPro;
using UnityEngine;

public class ScoreMB : MonoBehaviour
{
    public TMP_Text X;
    public TMP_Text O;

    private int _xScore;
    private int _oScore;

    public void AddScore(Marks wonMark)
    {
        if (wonMark == Marks.X) _xScore++;
        else _oScore++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        X.text = _xScore.ToString();
        O.text = _oScore.ToString();
    }

    public void ResetScore()
    {
        _xScore = 0;
        _oScore = 0;
        UpdateUI();
    }
}
