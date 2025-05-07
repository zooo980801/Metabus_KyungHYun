using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameState : MonoBehaviour
{
    public static GlobalGameState Instance;
    public Vector3 savedPlayerPosition = Vector3.zero;

    public bool returnToSelection = false;
    public string lastPlayedSceneName = "";

    public Vector2Int targetResolution = new Vector2Int(1280, 720);

    public int flappyScore = 0;
    public int lastFlappyScore = 0;
    public int flappyBestScore = 0;

    public int theStackScore = 0;
    public int theStackMaxCombo = 0;
    public int theStackBestScore = 0;
    public int theStackBestCombo = 0;

    public int topDowncurrentWave = 0;
    public int topDownBestWave = 0;

    private string savePath => Path.Combine(Application.persistentDataPath, "ScoreData.json");

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Screen.SetResolution(targetResolution.x, targetResolution.y, false);
    }

    public void SaveScore()
    {
        ScoreData data = new ScoreData
        {
            flappyBestScore = this.flappyBestScore,
            lastFlappyScore = this.lastFlappyScore,
            theStackBestScore = this.theStackBestScore,
            theStackBestCombo = this.theStackBestCombo,
            topDownBestWave = this.topDownBestWave,
            topDowncurrentWave = this.topDowncurrentWave
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadScore()
    {
        string path = savePath;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            try
            {
                ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                ApplyScoreData(data);
            }
            catch { }
        }
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>("ScoreData");
            if (textAsset != null)
            {
                try
                {
                    ScoreData data = JsonUtility.FromJson<ScoreData>(textAsset.text);
                    ApplyScoreData(data);
                    File.WriteAllText(path, textAsset.text);
                }
                catch { }
            }
        }
    }

    private void ApplyScoreData(ScoreData data)
    {
        flappyBestScore = data.flappyBestScore;
        lastFlappyScore = data.lastFlappyScore;
        theStackBestScore = data.theStackBestScore;
        theStackBestCombo = data.theStackBestCombo;
        topDownBestWave = data.topDownBestWave;
        topDowncurrentWave = data.topDowncurrentWave;
    }

    public void ResetScores()
    {
        flappyBestScore = 0;
        theStackBestScore = 0;
        theStackBestCombo = 0;
        topDownBestWave = 0;
        SaveScore();
    }
}
