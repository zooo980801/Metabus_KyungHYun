using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public enum UIState
{
    Home,
    Game,
    Score,
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;

    GameUI gameUI = null;

    ScoreUI scoreUI = null;

    TheStack theStack = null;
    private void Awake()
    {
        instance = this;
        theStack = FindObjectOfType<TheStack>();

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(UIState.Home);
    }


    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(UIState.Game);
    }

    public void OnClickExit()
    {


        GlobalGameState.Instance.returnToSelection = true;
        GlobalGameState.Instance.targetResolution = new Vector2Int(1280, 720);
        SceneManager.LoadScene("MainScene");
    }

    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }
    public void SetScoreUI()
    {
        int score = theStack.Score;
        int maxCombo = theStack.MaxCombo;
        int bestScore = theStack.BestScore;
        int bestCombo = theStack.BestCombo;

        // ✅ 점수 UI 표시
        scoreUI.SetUI(score, maxCombo, bestScore, bestCombo);

        if (GlobalGameState.Instance != null)
        {
            GlobalGameState.Instance.theStackScore = score;
            GlobalGameState.Instance.theStackMaxCombo = maxCombo;

            // ✅ 최고 점수 갱신 시만 저장
            bool updated = false;

            if (score > GlobalGameState.Instance.theStackBestScore)
            {
                GlobalGameState.Instance.theStackBestScore = score;
                updated = true;
            }

            if (maxCombo > GlobalGameState.Instance.theStackBestCombo)
            {
                GlobalGameState.Instance.theStackBestCombo = maxCombo;
                updated = true;
            }

            if (updated)
            {
                GlobalGameState.Instance.SaveScore();
            }
        }

        ChangeState(UIState.Score);
    }

}