using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class TopDownResourceController : MonoBehaviour
    {
        [SerializeField] private float healthChangeDelay = .5f;

        private TopDownBaseController baseController;
        private TopDownStatHandler statHandler;
        private TopDownAnimationHandler animationHandler;

        private float timeSinceLastChange = float.MaxValue;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => statHandler.Health;
        public AudioClip damageClip;

        private Action<float, float> OnChangeHealth;

        private void Awake()
        {
            statHandler = GetComponent<TopDownStatHandler>();
            animationHandler = GetComponent<TopDownAnimationHandler>();
            baseController = GetComponent<TopDownBaseController>();
        }

        private void Start()
        {
            CurrentHealth = statHandler.Health;
        }

        private void Update()
        {
            if (timeSinceLastChange < healthChangeDelay)
            {
                timeSinceLastChange += Time.deltaTime;
                if (timeSinceLastChange >= healthChangeDelay)
                {
                    animationHandler.InvincibilityEnd();
                }
            }
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
            timeSinceLastChange = float.MaxValue;
            OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        }
        public bool ChangeHealth(float change)
        {
            
            if (change == 0 || timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChange = 0f;
            CurrentHealth += change;
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

            OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

            if (change < 0)
            {
                animationHandler.Damage();
                if (damageClip != null)
                    TopDownSoundManager.PlayClip(damageClip);

            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }

            return true;
        }

        private void Death()
        {
            baseController.Death();

            if (TryGetComponent<TopDownPlayerController>(out var player))
            {
                TopDownGameManager.instance.GameOver();
            }
        }


        public void AddHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth += action;
        }

        public void RemoveHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth -= action;
        }
    }
}