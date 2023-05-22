using UnityEngine;

[ExecuteInEditMode]
public class GridderMaker : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject gridder;
    [Space]
    [Header("Properties")]
    [SerializeField] int count;
    [SerializeField] float xOffset;//there mustnt be more than one decimel ! cuz it will make more offset  
    [SerializeField] float yOffset;
    [Space]
    [Header("Button")]
    [SerializeField] int Active = 0;
    [SerializeField] bool button = false;

    //make level builer work on edit mode

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        xOffset = spriteRenderer.bounds.size.x;
        // removing extra decimlas
        xOffset = Mathf.Floor(xOffset * 10) / 10;
    }

    void Update()
    {
        if (Application.isPlaying) return;
        if (button && Active > 0)
        {
            button = false;
            MakeGridderPlatform();
        }
    }
    void MakeGridderPlatform()
    {
        GameObject parent = new GameObject("GridderGroup");
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(gridder);
            //what is the true way of position setting
            //
            obj.transform.position = transform.position;
            obj.transform.position += (new Vector3(xOffset, yOffset) * (i));

            obj.transform.SetParent(parent.transform);
        }
    }
}
