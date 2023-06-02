using UnityEngine;

public class Ladder : MonoBehaviour
{
    //we should remove leader  offset and calcultate this offset form playyers script half of sprite or htat we say extend
    [SerializeField] SpriteRenderer spritRenderer;
    public float endPos { get; private set; }
    public float startPos { get; private set; }
    public float endPosOffset;//this should be serilazible not public
    public float startPosOffset;//this should be serilazible not public
    public float leaderEnd;
    //how we should make broker leader logic 
    //player can go up but it stuck on certain height
    public bool isBroken = false;
    void Awake()
    {
        endPos = transform.position.y + spritRenderer.bounds.extents.y + endPosOffset;
        startPos = transform.position.y - (spritRenderer.bounds.extents.y + startPosOffset);

        leaderEnd = transform.position.y + spritRenderer.bounds.extents.y;
    }
    //public void SetStartPos(float leaderStartYValue)
    //    startPos = leaderStartYValue;
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(0, spritRenderer.bounds.extents.y + endPosOffset), .2f);
        Gizmos.DrawSphere(transform.position - new Vector3(0, spritRenderer.bounds.extents.y + startPosOffset), .2f);

    }
    // Get leader properties on player enters climb mode
}
