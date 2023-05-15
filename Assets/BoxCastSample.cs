using UnityEngine;

public class BoxCastSample : MonoBehaviour
{
    public float width = 1;//x
    public float height = 1;//y
    public Vector2 offset = new Vector2(0, 0);
    Color c = new Color();
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + offset, new Vector2(width, height), 0, Vector2.zero);
        if (hit.collider != null)
            c = Color.green;
        else
            c = Color.red;

        Debug.DrawRay((Vector2)transform.position + offset + new Vector2(-width / 2, height / 2), new Vector2(width, 0), c);
        Debug.DrawRay((Vector2)transform.position + offset + new Vector2(-width / 2, -height / 2), new Vector2(width, 0), c);
        Debug.DrawRay((Vector2)transform.position + offset + new Vector2(-width / 2, -height / 2), new Vector2(0, height), c);
        Debug.DrawRay((Vector2)transform.position + offset + new Vector2(width / 2, -height / 2), new Vector2(0, height), c);

    }
    void OnDrawGizmos()
    {
        Gizmos.DrawCube((Vector2)transform.position + offset, new Vector2(width, height));
        Gizmos.color = c;
    }
}
