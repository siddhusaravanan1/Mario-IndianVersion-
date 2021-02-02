using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    float newX;
    // Update is called once per frame
    void LateUpdate()
    {
        newX = player.transform.position.x;
        if (newX < -2.9f)
        {
            newX = -3f;
        }
        if (newX > 162)
        {
            newX = 163;
        }

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
