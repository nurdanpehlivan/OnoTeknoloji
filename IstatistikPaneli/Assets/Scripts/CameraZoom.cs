using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target; // Yakýnlaþtýrýlacak hedef
    public float zoomSpeed = 1f; // Yakýnlaþtýrma hýzý
    public float zoomDistance = 10f; // Yakýnlaþtýrma mesafesi

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Kameranýn baþlangýç pozisyonunu ve rotasýný sakla
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void StartZooming()
    {
        // Kamera yakýnlaþtýrma iþlemini baþlat
        StopAllCoroutines();
        StartCoroutine(ZoomIn());
    }

    public void ResetCameraPosition()
    {
        // Kamera pozisyonunu baþlangýç pozisyonuna döndür
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomIn()
    {
        // Yakýnlaþtýrma iþlemi
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 targetPosition = target.position - target.forward * zoomDistance;

        while (elapsedTime < zoomSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / zoomSpeed));
            transform.rotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(target.position - transform.position), (elapsedTime / zoomSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    private IEnumerator ZoomOut()
    {
        // Kamera pozisyonunu baþlangýç pozisyonuna döndür
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < zoomSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, originalPosition, (elapsedTime / zoomSpeed));
            transform.rotation = Quaternion.Slerp(startRotation, originalRotation, (elapsedTime / zoomSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}