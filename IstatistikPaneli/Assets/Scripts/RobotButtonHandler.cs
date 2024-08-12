using UnityEngine;

public class RobotButtonHandler : MonoBehaviour
{
    public CameraZoom cameraZoom; 
    public Transform robotTarget; 
    public GameObject AnaPanel; 

    void Start()
    {
        if (AnaPanel != null)
        {
            AnaPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("AnaPanel referansý atanmadý.");
        }

        // Referanslarý kontrol et
        if (cameraZoom == null)
        {
            Debug.LogError("CameraZoom referansý atanmadý.");
        }

        if (robotTarget == null)
        {
            Debug.LogError("RobotTarget referansý atanmadý.");
        }

        // Program baþlar baþlamaz kamerayý üstten baþlat
        if (cameraZoom != null && robotTarget != null)
        {
            Vector3 startPosition = new Vector3(robotTarget.position.x, robotTarget.position.y + 10, robotTarget.position.z - 10);
            Quaternion startRotation = Quaternion.Euler(45, 0, 0); // Kamerayý tepeden bakacak þekilde ayarla
            cameraZoom.transform.position = startPosition;
            cameraZoom.transform.rotation = startRotation;
        }
    }

    void OnMouseDown()
    {
        if (cameraZoom != null && robotTarget != null)
        {
            Debug.Log("Robota týklandý.");

            // Kamera hedefe biraz daha yakýn ama daha uzakta olacak þekilde pozisyonu hesapla
            Vector3 targetPosition = robotTarget.position + new Vector3(0, 3, -8); // Yakýnlaþtýrma mesafesi ve yüksekliði
            Quaternion targetRotation = Quaternion.LookRotation(robotTarget.position - targetPosition); // Robotu hedefle

            // Kamera yakýnlaþtýrma iþlemini baþlat
            cameraZoom.StartZooming(targetPosition, targetRotation, this);

            ShowAnaPanel();
        }
    }

    public void ShowAnaPanel()
    {
        if (AnaPanel != null)
        {
            Debug.Log("AnaPanel gösteriliyor.");
            AnaPanel.SetActive(true);

            Transform parent = AnaPanel.transform.parent;
            while (parent != null)
            {
                if (!parent.gameObject.activeSelf)
                {
                    Debug.LogWarning($"Parent {parent.gameObject.name} aktif deðil.");
                    parent.gameObject.SetActive(true);
                }
                parent = parent.parent;
            }

            // Ek hata ayýklama adýmlarý
            if (AnaPanel.activeSelf)
            {
                Debug.Log("AnaPanel aktif durumda.");
            }
            else
            {
                Debug.LogWarning("AnaPanel aktif edilmesine raðmen hala pasif.");
            }
        }
        else
        {
            Debug.LogError("AnaPanel referansý atanmýþ ancak GameObject bulunamadý.");
        }
    }



    public void HideAnaPanel()
    {
        if (AnaPanel != null)
        {
            AnaPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("AnaPanel referansý atanmýþ ancak GameObject bulunamadý.");
        }
    }
}
