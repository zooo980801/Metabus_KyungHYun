using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace TopDown
{
    public class TopDownHomeUI : TopDownBaseUI
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        public override void Init(TopDownUIManager topDownuiManager)
        {
            base.Init(topDownuiManager);
            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        public void OnClickStartButton()
        {
            TopDownGameManager.instance.StartGame();
        }

        public void OnClickExitButton()
        {

            GlobalGameState.Instance.returnToSelection = true;


            SceneManager.LoadScene("MainScene");
        }

        protected override TopDownUIState GetUIState()
        {
            return TopDownUIState.Home;
        }
    }
}