using UnityEngine;

public class JumpWithAnimEducation : MonoBehaviour
{
    public LayerMask layerMask;
    private void Update()
    {
        HammerDestoryingCast();
    }
    void HammerDestoryingCast()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2, Vector2.zero, 0, layerMask);
        if (hit)
        {
            hit.collider.gameObject.SetActive(false);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, .2f);
        Gizmos.DrawSphere(transform.position, 2);
    }

}