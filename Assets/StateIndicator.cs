using UnityEngine;
using UnityEngine.UI;

public class StateIndicator : MonoBehaviour
{
    [SerializeField] Player player;
    Text textComponent;
    void Start()
    {
        textComponent = GetComponent<Text>();
        player.stateManager.OnStateChanged += ChangeText;
    }

    void ChangeText()
    {
        textComponent.text = player.stateManager.currentState.ToString();
    }
    private void OnDisable()
    {
        player.stateManager.OnStateChanged -= ChangeText;
    }

}
