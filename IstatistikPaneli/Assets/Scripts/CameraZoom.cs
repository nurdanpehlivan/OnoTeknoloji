using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2f; // Yakınlaşma süresi
    public float targetDistance = 5f; // Yakınlaşmak istediğiniz mesafe
    private Camera camera;
    private float initialDistance;
    private bool isZooming = false;

    void Start()
    {
        camera = Camera.main;
        initialDistance = Vector3.Distance(camera.transform.position, transform.position);
    }

    public void StartZooming()
    {
        if (!isZooming)
        {
            StartCoroutine(ZoomIn());
        }
    }

    IEnumerator ZoomIn()
    {
        isZooming = true;
        float elapsedTime = 0f;
        float startDistance = Vector3.Distance(camera.transform.position, transform.position);

        while (elapsedTime < zoomSpeed)
        {
            float newDistance = Mathf.Lerp(startDistance, targetDistance, elapsedTime / zoomSpeed);
            camera.transform.position = transform.position + new Vector3(0, 0, -newDistance);
            camera.transform.LookAt(transform);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        camera.transform.position = transform.position + new Vector3(0, 0, -targetDistance);
        camera.transform.LookAt(transform);
        isZooming = false;
    }
}
