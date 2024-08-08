using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target; // Yak�nla�t�r�lacak hedef
    public float zoomSpeed = 1f; // Yak�nla�t�rma h�z�
    public float zoomDistance = 10f; // Yak�nla�t�rma mesafesi

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Kameran�n ba�lang�� pozisyonunu ve rotas�n� sakla
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void StartZooming()
    {
        // Kamera yak�nla�t�rma i�lemini ba�lat
        StopAllCoroutines();
        StartCoroutine(ZoomIn());
    }

    public void ResetCameraPosition()
    {
        // Kamera pozisyonunu ba�lang�� pozisyonuna d�nd�r
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomIn()
    {
        // Yak�nla�t�rma i�lemi
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
        // Kamera pozisyonunu ba�lang�� pozisyonuna d�nd�r
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