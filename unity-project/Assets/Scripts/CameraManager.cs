using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject cameraMainPosition;

    public void MoveCamera(GameObject view)
    {
        camera.transform.position = view.transform.position;
        camera.transform.rotation = view.transform.rotation;
    }

    void Start()
    {
        camera.transform.position = cameraMainPosition.transform.position;
        camera.transform.rotation = cameraMainPosition.transform.rotation;
    }

}