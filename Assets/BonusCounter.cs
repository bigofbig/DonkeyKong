using TMPro;
using UnityEngine;

public class BonusCounter : MonoBehaviour
{
    int remainingValue = 5000;
    [SerializeField] TMP_Text textMeshPro;
    void Start()
    {
        InvokeRepeating(nameof(DecreaseValue), 1, 1);
    }
    void DecreaseValue()
    {
        remainingValue -= 100;
        //if value is zero or less , call gameover and Return
        if (remainingValue <= 0)
        {
            //call game over 
            FindObjectOfType<GameOver>().OnGameOver();
            return;
        }
        string currentValueStringed = remainingValue.ToString();
        //how much zero this value need behind 
        int zeroesRequiresBehindText = 5 - currentValueStringed.Length;
        string zerosPlaceHolder = "";
        if (zeroesRequiresBehindText > 0)
        {
            for (int i = 0; i < zeroesRequiresBehindText; i++)
            {
                zerosPlaceHolder += 0;
            }
        }
        string valueAndRequriedZerosBehindIt = zerosPlaceHolder + currentValueStringed;

        textMeshPro.text = valueAndRequriedZerosBehindIt;
    }

    //if value is zero call game over 


}
