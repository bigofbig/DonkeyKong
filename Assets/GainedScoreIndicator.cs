using TMPro;
using UnityEngine;
public class GainedScoreIndicator : MonoBehaviour
{
    public static GainedScoreIndicator current;
    [SerializeField] TMP_Text textMeshPro;
    [SerializeField] GameObject indicator;
    Player player;
    //mario jump is not relayable since it changes from time to time , mybe i should do it with transfom not physics
    // after endeing a ladder if uou keep up key and then move you may fall through gridder mybe its just leader (3) problem
    //mario head can hit to top floor barrel or flame and cuase him death
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
