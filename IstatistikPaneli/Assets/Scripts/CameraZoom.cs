using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target; // Yakınlaştırılacak hedef
    public float zoomSpeed = 1f; // Yakınlaştırma hızı
    public float zoomDistance = 10f; // Yakınlaştırma mesafesi

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Kameranın başlangıç pozisyonunu ve rotasını sakla
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void StartZooming()
    {
        // Kamera yakınlaştırma işlemini başlat
        StopAllCoroutines();
        StartCoroutine(ZoomIn());
    }

    public void ResetCameraPosition()
    {
        // Kamera pozisyonunu başlangıç pozisyonuna döndür
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomIn()
    {
        // Yakınlaştırma işlemi
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 targetPosition = target.position - target.forward * zoomDistance;

        while (elapsedTime < zoomSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / zoomSpeed));
            Vector3 directionToTarget = target.position - transform.position;
            
            // Yön vektörünü kontrol edin
            if (directionToTarget.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(directionToTarget), (elapsedTime / zoomSpeed));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Vector3 finalDirection = target.position - transform.position;
        
        // Yön vektörünü kontrol edin
        if (finalDirection.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(finalDirection);
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
            transform.position = Vector3.Lerp(startPosition, originalPosition, (elapsedTime / zoomSpeed));
            transform.rotation = Quaternion.Slerp(startRotation, originalRotation, (elapsedTime / zoomSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
