using UnityEngine;

public class GridderMaker : MonoBehaviour
{
    [SerializeField] GameObject gridder;
    [SerializeField] int count;
    [SerializeField] float xOffset;//there mustnt be more than one decimel ! cuz it will make more offset  
    [SerializeField] float yOffset;

    SpriteRenderer spriteRenderer;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeGridderPlatform();
        }
    }
    void MakeGridderPlatform()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(gridder);
            obj.transform.position += (new Vector3(xOffset, yOffset) * (i + 1));

        }
    }
}
