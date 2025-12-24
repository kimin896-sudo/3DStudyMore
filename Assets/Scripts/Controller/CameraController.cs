using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 delta;
    public GameObject player;
    void Start()
    {
        delta = delta - player.transform.position;
    }

    void Update()
    {
        if (Physics.Raycast(player.transform.position, delta, out RaycastHit hit, delta.magnitude,LayerMask.GetMask("Wall")))
        {
            float dist = (hit.point - player.transform.position).magnitude * 0.8f;
            transform.position = player.transform.position+Vector3.up + delta.normalized * dist;
        }
        else
        {
            transform.position = player.transform.position + delta;

            transform.LookAt(player.transform);
        }
    }
}
