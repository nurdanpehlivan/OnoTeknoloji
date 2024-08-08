using UnityEngine;

public class RobotButtonHandler : MonoBehaviour
{
    public CameraZoom cameraZoom; // CameraZoom scriptinin referansý

    void Start()
    {
        if (cameraZoom == null)
        {
            Debug.LogError("CameraZoom referansý atanmadý.");
        }
    }

    void OnMouseDown()
    {
        if (cameraZoom != null)
        {
            Debug.Log("Robota týklandý.");
            cameraZoom.StartZooming(); // Kamerayý yakýnlaþtýr
        }
    }
}
