using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HighScoreText : MonoBehaviour
{
    private static int highScore;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "HighScore : " + highScore;
        GameManager.OnCubeSpawned += GameManager_OnCubeSpawned;
    }

    private void GameManager_OnCubeSpawned()
    {
        text.text = "";
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubeSpawned;
    }

    private void Update()
    {
        if (ScoreText.score > highScore)
        {
            highScore = ScoreText.score;
        }
    }
}
