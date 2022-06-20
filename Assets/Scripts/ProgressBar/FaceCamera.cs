using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera Camera;


    private void Start()
    {
        Camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        transform.LookAt(Camera.transform, Vector3.up);
    }
}
