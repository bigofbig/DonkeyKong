using UnityEngine;

public class Leader : MonoBehaviour
{
    [SerializeField] SpriteRenderer spritRenderer;
    public float endPos { get; private set; }
    public float startPos { get; private set; }
    public float endPosOffset;
    public float startPosOffset;
    void Awake()
    {
        endPos = transform.position.y + spritRenderer.bounds.extents.y + endPosOffset;
        startPos = transform.position.y - spritRenderer.bounds.extents.y - startPosOffset;
    }
    //public void SetStartPos(float leaderStartYValue)
    //    startPos = leaderStartYValue;
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(0, spritRenderer.bounds.extents.y + endPosOffset), .2f);
        Gizmos.DrawSphere(transform.position - new Vector3(0, spritRenderer.bounds.extents.y - startPosOffset), .2f);

    }
    // Get leader properties on player enters climb mode
}
