using UnityEngine;

public class SpawnGizmos : MonoBehaviour
{
    public Color _color = Color.yellow;
    public float _radius = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
