using UnityEngine;

public class GameSelectUIController : MonoBehaviour
{
    [SerializeField] private GameSelectUIManager uiManager;

    public void OnClickFlappyPlane()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GlobalGameState.Instance.savedPlayerPosition = player.transform.position;
        }
        GlobalGameState.Instance.targetResolution = new Vector2Int(1280, 720);
        GlobalGameState.Instance.returnToSelection = true;
        GlobalGameState.Instance.lastPlayedSceneName = "FlappyPlane";
        uiManager.CloseSelectionPanel();
        UnityEngine.SceneManagement.SceneManager.LoadScene("FlappyPlane");
    }

    public void OnClickTheStack()
    {

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GlobalGameState.Instance.savedPlayerPosition = player.transform.position;
        }
        GlobalGameState.Instance.returnToSelection = true;
        GlobalGameState.Instance.lastPlayedSceneName = "TheStack";
        GlobalGameState.Instance.targetResolution = new Vector2Int(540, 960);
        uiManager.CloseSelectionPanel();
        UnityEngine.SceneManagement.SceneManager.LoadScene("TheStack");


    }

    public void OnClickTopDown()
    {

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GlobalGameState.Instance.savedPlayerPosition = player.transform.position;
        }
        GlobalGameState.Instance.targetResolution = new Vector2Int(1280, 720);
        GlobalGameState.Instance.returnToSelection = true;
        GlobalGameState.Instance.lastPlayedSceneName = "TopDown";
        uiManager.CloseSelectionPanel();
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopDown");
    }

    public void OnClickCancel()
    {
        uiManager.CloseSelectionPanel();
    }
}
