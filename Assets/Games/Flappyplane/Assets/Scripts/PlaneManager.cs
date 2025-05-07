using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel; 
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    private void Start()
    {

        if (scoreText == null)
        {
            return;
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ShowGameOverUI(int currentScore, int bestScore)
    {

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (scoreText != null)
            scoreText.gameObject.SetActive(false);

        if (currentScoreText != null)
            currentScoreText.text = $"현재 점수: {currentScore}";

        if (bestScoreText != null)
            bestScoreText.text = $"최고 점수: {bestScore}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // 버튼에서 호출
    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        GameManager.Instance.RestartGame();
    }

    public void OnClickGoToMain()
    {

        if (GlobalGameState.Instance != null)
        {
            GlobalGameState.Instance.returnToSelection = true;

        }

        SceneManager.LoadScene("MainScene");
    }
}