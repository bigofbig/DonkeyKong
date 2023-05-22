using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridderModifier : MonoBehaviour
{
    [SerializeField] List<GameObject> gridders;
    void Start()
    {
        gridders.AddRange(Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Grider(Clone)"));
        foreach (var gridder in gridders)
        {
            BoxCollider2D collider = gridder.GetComponent<BoxCollider2D>();
            collider.usedByEffector = true;
            collider.offset = new Vector2(0, .32f);
            collider.size = new Vector2(1.45f, .06f);

            PlatformEffector2D effector = gridder.AddComponent<PlatformEffector2D>();
            effector.surfaceArc = 80;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
