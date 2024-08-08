using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    void Start()
    {
        // Ba�lang�� pozisyonunu, rotas�n� ve �l�e�ini kaydet
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Plane'in pozisyonunu, rotas�n� ve �l�e�ini ba�lang�� de�erlerinde tut
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
    }
}
