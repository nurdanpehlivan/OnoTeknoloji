using UnityEngine;

public class RobotButtonHandler : MonoBehaviour
{
    public CameraZoom cameraZoom; // CameraZoom scriptinin referans�

    void Start()
    {
        if (cameraZoom == null)
        {
            Debug.LogError("CameraZoom referans� atanmad�.");
        }
    }

    void OnMouseDown()
    {
        if (cameraZoom != null)
        {
            Debug.Log("Robota t�kland�.");
            cameraZoom.StartZooming(); // Kameray� yak�nla�t�r
        }
    }
}
