using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    public Transform target; // Yakınlaştırılacak hedef
    public float zoomSpeed = 1f; // Yakınlaştırma süresi
    public float zoomDistance = 10f; // Yakınlaştırma mesafesi

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private RobotButtonHandler robotButtonHandler; // RobotButtonHandler referansı

    void Start()
    {
        // Kameranın başlangıç pozisyonunu ve rotasını sakla
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void StartZooming(Vector3 targetPosition, Quaternion targetRotation, RobotButtonHandler handler)
    {
        robotButtonHandler = handler;
        // Kamera yakınlaştırma işlemini başlat
        StopAllCoroutines();
        StartCoroutine(ZoomIn(targetPosition, targetRotation));
    }

    public void ResetCameraPosition()
    {
        // Kamera pozisyonunu başlangıç pozisyonuna döndür
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomIn(Vector3 targetPosition, Quaternion targetRotation)
    {
        // Yakınlaştırma işlemi
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < zoomSpeed)
        {
            float t = elapsedTime / zoomSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Hedef pozisyona ve rotaya ulaş
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        // AnaPanel'i ekrana getir
        if (robotButtonHandler != null)
        {
            robotButtonHandler.ShowAnaPanel();
        }
    }

    private IEnumerator ZoomOut()
    {
        // Kamera pozisyonunu başlangıç pozisyonuna döndür
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < zoomSpeed)
        {
            float t = elapsedTime / zoomSpeed;
            transform.position = Vector3.Lerp(startPosition, originalPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, originalRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Başlangıç pozisyonuna ve rotaya ulaş
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
