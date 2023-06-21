using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter current;
    [SerializeField] TMP_Text textMeshPro;
    int score = 0;
    void Awake()
    {
        current = this;
    }
    public void AddScore(int scoreToAdd)
    {
        GainedScoreIndicator.current.OnScoreGained(scoreToAdd);
        score += scoreToAdd;
        //add zeros before score
        string scoreStringed = score.ToString();
        string zeroes = "";
        if (scoreStringed.Length < 6)
        {
            int zeroesNeededToBeAdded = 6 - scoreStringed.Length;
            for (int i = 0; i < zeroesNeededToBeAdded; i++)
            {
                zeroes += "0";
            }
        }

        textMeshPro.text = "";
        textMeshPro.text = zeroes + score;
    }
}
