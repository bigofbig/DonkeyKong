using System.Collections.Generic;
using UnityEngine;

public class GirderGroup : MonoBehaviour
{
    [SerializeField] List<GameObject> griders;
    [SerializeField] List<Vector2> gridersMainPos;
    [SerializeField] GameObject refrenceHeight;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            griders.Add(transform.GetChild(i).gameObject);
            gridersMainPos.Add(transform.GetChild(i).position);
        }
        LineUpAllGriders();

    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //        LineUpAllGriders();
    //    if (Input.GetKeyDown(KeyCode.S))
    //        SetAllGridersToMainPos();
    //}

    public void SetAllGridersToMainPos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            griders[i].transform.position = gridersMainPos[i];
        }
    }

    void LineUpAllGriders()
    {
        foreach (var grider in griders)
        {
            grider.transform.position = new Vector2(grider.transform.position.x, refrenceHeight.transform.position.y);
        }
    }
}
