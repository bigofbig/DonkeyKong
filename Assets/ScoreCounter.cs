using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter current;
    [SerializeField] TMP_Text  textMeshPro;
    int score = 0;
    void Awake()
    {
        current = this;
    }
    public void AddScore(int scoreToAdd)
    {
        GainedScoreIndicator.current.OnScoreGained(scoreToAdd);
        score += scoreToAdd;
        textMeshPro.text = score.ToString();
    }
}
