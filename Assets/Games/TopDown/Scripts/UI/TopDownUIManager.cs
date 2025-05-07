using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public enum TopDownUIState
    {
        Home,
        Game,
        GameOver,
    }

    public class TopDownUIManager : MonoBehaviour
    {
        TopDownHomeUI homeUI;
        TopDownGameUI gameUI;
        TopDownGameOverUI gameOverUI;
        private TopDownUIState currentState; // 현재 UI 상태

        private void Awake()
        {
            // 자식 오브젝트에서 각각의 UI를 찾아 초기화
            homeUI = GetComponentInChildren<TopDownHomeUI>(true);
            homeUI.Init(this);
            gameUI = GetComponentInChildren<TopDownGameUI>(true);
            gameUI.Init(this);
            gameOverUI = GetComponentInChildren<TopDownGameOverUI>(true);
            gameOverUI.Init(this);

            // 초기 상태를 홈 화면으로 설정
            ChangeState(TopDownUIState.Home);
        }

        public void SetPlayGame()
        {
            ChangeState(TopDownUIState.Game);
        }

        public void SetGameOver()
        {
            ChangeState(TopDownUIState.GameOver);
        }

        public void ChangeWave(int waveIndex)
        {
            gameUI.UpdateWaveText(waveIndex);
        }

        public void ChangePlayerHP(float currentHP, float maxHP)
        {
            gameUI.UpdateHPSlider(currentHP / maxHP);
        }

        // 현재 UI 상태를 변경하고, 각 UI 오브젝트에 상태를 전달
        public void ChangeState(TopDownUIState state)
        {
            currentState = state;

            // 각 UI가 자신이 보여져야 할 상태인지 판단하고 표시 여부 결정
            homeUI.SetActive(currentState);
            gameUI.SetActive(currentState);
            gameOverUI.SetActive(currentState);

            if (state == TopDownUIState.GameOver)
            {
                gameOverUI.SetWaveResultUI(); // ✅ 점수 반영!
            }
        }
    }
}