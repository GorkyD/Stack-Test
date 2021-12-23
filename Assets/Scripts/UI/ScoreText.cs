using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI text;

    private void Start()
    {
        score = 0;
        text = GetComponent<TMPro.TextMeshProUGUI > ();
        GameManager.OnCubeSpawned += GameManager_OnCubeSpawned;
    }

    private void GameManager_OnCubeSpawned()
    {
        text.text = "Score: " + score;
        score++;
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubeSpawned;
    }

    public int GetScore()
    {
        return score - 1;
    }
}
