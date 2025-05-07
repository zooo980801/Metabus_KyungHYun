using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flappyScoreText;
    [SerializeField] private TextMeshProUGUI theStackScoreText;
    [SerializeField] private TextMeshProUGUI topDownScoreText;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
    }

    public void Open()
    {
        RefreshScore();
        gameObject.SetActive(true);
    }

    public void RefreshScore()
    {
        if (GlobalGameState.Instance == null)
        {
            Debug.LogWarning("GlobalGameState is null - 점수 갱신 불가");
            return;
        }

        flappyScoreText.text = $"FlappyPlane 점수: {GlobalGameState.Instance.flappyBestScore}";
        theStackScoreText.text = $"TheStack 점수: {GlobalGameState.Instance.theStackBestScore} | 최고 콤보: {GlobalGameState.Instance.theStackBestCombo}";
        topDownScoreText.text = $"TopDown 최고 Wave: {GlobalGameState.Instance.topDownBestWave}";
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
