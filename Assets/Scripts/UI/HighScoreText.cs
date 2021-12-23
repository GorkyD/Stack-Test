using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    [SerializeField] private GameObject scoreGameObject;
    private TextMeshProUGUI text;
    private ScoreText ScoreTextObject;

    private int highScore = 0;
    private int score = 0;
    private string highScoreKey = "HighScore";

    private void OnEnable()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    private void Start()
    {
        ScoreTextObject = scoreGameObject.GetComponent<ScoreText>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "HighScore : " + highScore;
        GameManager.OnCubeSpawned += GameManager_OnCubeSpawned;
    }

    private void GameManager_OnCubeSpawned()
    {
        text.text = "";
    }

    private void Update()
    {
        score = ScoreTextObject.GetScore();
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubeSpawned;
    }

    private void OnDisable()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}
