using TMPro;
using UnityEngine;
public class GainedScoreIndicator : MonoBehaviour
{
    public static GainedScoreIndicator current;
    [SerializeField] TMP_Text textMeshPro;
    [SerializeField] GameObject indicator;
    Player player;
    float distanceFromPlayer = -2f;

    void Awake()
    {
        current = this;
        player = FindObjectOfType<Player>();
        DisableIndicator();
    }
    public void OnScoreGained(int gainedScoreValue)
    {
        textMeshPro.text = gainedScoreValue.ToString();
        transform.position = player.transform.position + new Vector3(0, distanceFromPlayer);
        indicator.SetActive(true);
        Invoke(nameof(DisableIndicator), .7f);
    }
    void DisableIndicator()
    {
        indicator.SetActive(false);
    }
}
