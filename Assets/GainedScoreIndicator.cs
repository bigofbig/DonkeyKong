using TMPro;
using UnityEngine;
public class GainedScoreIndicator : MonoBehaviour
{
    public static GainedScoreIndicator current;
    [SerializeField] TMP_Text textMeshPro;
    [SerializeField] GameObject indicator;
    Player player;
    void Awake()
    {
        current = this;
        player = FindObjectOfType<Player>();
        DisableIndicator();
    }
    public void OnScoreGained(int gainedScoreValue)
    {
        textMeshPro.text = gainedScoreValue.ToString();
        transform.position = player.transform.position + new Vector3(0, -2.5f);
        indicator.SetActive(true);
        Invoke(nameof(DisableIndicator), .7f);
    }
    void DisableIndicator()
    {
        indicator.SetActive(false);
    }
}
