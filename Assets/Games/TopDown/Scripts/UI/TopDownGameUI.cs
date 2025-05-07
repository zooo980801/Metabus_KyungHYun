using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace TopDown
{
    public class TopDownGameUI : TopDownBaseUI
    {
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private Slider hpSlider;

        private void Start()
        {
            UpdateHPSlider(1);
        }

        public void UpdateHPSlider(float percentage)
        {
            hpSlider.value = percentage;
        }

        public void UpdateWaveText(int wave)
        {
            waveText.text = wave.ToString();
        }

        protected override TopDownUIState GetUIState()
        {
            return TopDownUIState.Game;
        }
    }
}