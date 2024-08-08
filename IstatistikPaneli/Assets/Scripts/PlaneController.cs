using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    void Start()
    {
        // Baþlangýç pozisyonunu, rotasýný ve ölçeðini kaydet
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Plane'in pozisyonunu, rotasýný ve ölçeðini baþlangýç deðerlerinde tut
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
    }
}
