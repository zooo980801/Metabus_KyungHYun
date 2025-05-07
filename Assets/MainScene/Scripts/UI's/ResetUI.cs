using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUI : MonoBehaviour
{
    [SerializeField] private GameObject resetConfirmPanel;

    public void OpenResetConfirmPanel()
    {
        resetConfirmPanel?.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseResetConfirmPanel()
    {
        resetConfirmPanel?.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ConfirmReset()
    {
        GlobalGameState.Instance?.ResetScores();
        FindObjectOfType<ScoreBoardUI>()?.RefreshScore();
        CloseResetConfirmPanel();
    }
}