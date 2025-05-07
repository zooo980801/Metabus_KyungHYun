using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace TopDown
{
    public class TopDownGameOverUI : TopDownBaseUI
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI currentWaveText;
        [SerializeField] private TextMeshProUGUI bestWaveText;

        public override void Init(TopDownUIManager uiManager)
        {
            base.Init(topDownuiManager);
            restartButton.onClick.AddListener(OnClickRestartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        public void SetWaveResultUI()
        {
            int current = GlobalGameState.Instance?.topDowncurrentWave ?? -1;
            int best = GlobalGameState.Instance?.topDownBestWave ?? -1;

            currentWaveText.text = $"현재 도달 층수 : {current}";
            bestWaveText.text = $"최고 도달 층수 : {best}";
        }

        public void OnClickRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnClickExitButton()
        {

            GlobalGameState.Instance.returnToSelection = true;
            SceneManager.LoadScene("MainScene");
        }

        protected override TopDownUIState GetUIState()
        {
            return TopDownUIState.GameOver;
        }
    }
}