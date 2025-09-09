using UnityEngine;

public class TppCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 3, -5);

    void LateUpdate()
    {
        if (player == null) return;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
