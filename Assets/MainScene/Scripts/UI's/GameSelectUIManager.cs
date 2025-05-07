using System.Collections;
using UnityEngine;

public class GameSelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameSelectPanel;
    [SerializeField] private ScoreBoardUI scoreBoardUI;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GlobalGameState.Instance != null);

        scoreBoardUI.RefreshScore();

        if (GlobalGameState.Instance.returnToSelection)
        {
            GlobalGameState.Instance.returnToSelection = false;
            OpenSelectionPanel();
        }
    }

    public void OpenSelectionPanel()
    {
        gameSelectPanel?.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSelectionPanel()
    {
        gameSelectPanel?.SetActive(false);
        Time.timeScale = 1f;
    }
}
