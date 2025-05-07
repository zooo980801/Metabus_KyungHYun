using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace TopDown
{
    public class TopDownGameManager : MonoBehaviour
    {
        public static TopDownGameManager instance;

        public TopDownPlayerController player { get; private set; }
        private TopDownResourceController _playerResourceController;

        [SerializeField] private int currentWaveIndex = 0;

        private TopDownStatHandler statHandler;
        private TopDownEnemyManager enemyManager;
        private TopDownUIManager topDownuiManager;
        public static bool isFirstLoading = true;

        public float MaxHealth => statHandler.Health;


        private void Awake()
        {
            instance = this;
            isFirstLoading = false;
            player = FindObjectOfType<TopDownPlayerController>();
            player.Init(this);

            topDownuiManager = FindObjectOfType<TopDownUIManager>();

            enemyManager = GetComponentInChildren<TopDownEnemyManager>();
            enemyManager.Init(this);

            _playerResourceController = player.GetComponent<TopDownResourceController>();
            _playerResourceController.RemoveHealthChangeEvent(topDownuiManager.ChangePlayerHP);
            _playerResourceController.AddHealthChangeEvent(topDownuiManager.ChangePlayerHP);
        }

        private void Start()
        {
            player.GetComponent<TopDownResourceController>().ResetHealth();
            topDownuiManager.SetPlayGame();
            StartGame();
        }


        public void StartGame()
        {
            currentWaveIndex = 0;
            player.GetComponent<TopDownResourceController>().ResetHealth();
            topDownuiManager.SetPlayGame();
            StartNextWave();
        }

        void StartNextWave()
        {
            currentWaveIndex += 1;
            topDownuiManager.ChangeWave(currentWaveIndex);
            enemyManager.StartWave(1 + currentWaveIndex / 5);
        }

        public void EndOfWave()
        {
            StartNextWave();
        }

        public void GameOver()
        {
            enemyManager.StopWave();
            topDownuiManager.ChangeState(TopDownUIState.GameOver);

            if (GlobalGameState.Instance != null)
            {
                GlobalGameState.Instance.topDowncurrentWave = currentWaveIndex;

                bool updated = false;

                if (currentWaveIndex > GlobalGameState.Instance.topDownBestWave)
                {
                    GlobalGameState.Instance.topDownBestWave = currentWaveIndex;
                    updated = true;
                }

                if (updated)
                {
                    GlobalGameState.Instance.SaveScore();
                }
            }
        }


    }
}