using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public abstract class TopDownBaseUI : MonoBehaviour
    {
        protected TopDownUIManager topDownuiManager;

        public virtual void Init(TopDownUIManager uiManager)
        {
            this.topDownuiManager = uiManager;
        }

        protected abstract TopDownUIState GetUIState();

        public void SetActive(TopDownUIState state)
        {
            this.gameObject.SetActive(GetUIState() == state);
        }
    }
}