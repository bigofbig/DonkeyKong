using System.Collections.Generic;
using UnityEngine;

public class GridderGroupsManager : MonoBehaviour
{
    [SerializeField] List<GirderGroup> griderGroup;
    int i = 0;
    public void ChangeGriderGroupShape()
    {
        if (i > griderGroup.Count) { Debug.Log("Request is more than available grider group"); return; }
        griderGroup[i].SetAllGridersToMainPos();
        i++;
    }
}
