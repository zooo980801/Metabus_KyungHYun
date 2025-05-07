using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    // Start is called before the first frame update
    public class GameManager : MonoBehaviour
    {
        static GameManager gameManager;

        public static GameManager Instance { get { return gameManager; } }

        PlaneManager planeManager;
        private int currentScore = 0;

    public PlaneManager UIManager
    {
        get { return planeManager; }
    }


    private void Awake()
        {
            gameManager = this;
        planeManager = FindObjectOfType<PlaneManager>();
        }

    private void Start()
    {
        planeManager.UpdateScore(0);
    }

    public void GameOver()
    {
        if (GlobalGameState.Instance != null)
        {
            GlobalGameState.Instance.flappyScore = currentScore;

            bool updated = false;
            if (currentScore > GlobalGameState.Instance.flappyBestScore)
            {
                GlobalGameState.Instance.flappyBestScore = currentScore;
                updated = true;
            }

            if (updated)
            {
                GlobalGameState.Instance.SaveScore();
            }

            planeManager.ShowGameOverUI(currentScore, GlobalGameState.Instance.flappyBestScore);
        }
        else
        {
            planeManager.ShowGameOverUI(currentScore, currentScore); // fallback
        }

    }



    public void RestartGame()
        {
        if (GlobalGameState.Instance != null)
        {
            GlobalGameState.Instance.lastFlappyScore = currentScore;
            GlobalGameState.Instance.SaveScore();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

        public void AddScore(int score)
        {
            currentScore += score;
        planeManager.UpdateScore(currentScore);
        }

    }