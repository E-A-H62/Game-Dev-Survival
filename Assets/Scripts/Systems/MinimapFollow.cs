using UnityEngine;

public class MinimapFollow : MonoBehaviour {
    public Transform player;

    void LateUpdate() {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // Keep current camera height
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
