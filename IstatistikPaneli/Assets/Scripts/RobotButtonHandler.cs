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
            Debug.LogError("AnaPanel referans� atanmad�.");
        }

        // Referanslar� kontrol et
        if (cameraZoom == null)
        {
            Debug.LogError("CameraZoom referans� atanmad�.");
        }

        if (robotTarget == null)
        {
            Debug.LogError("RobotTarget referans� atanmad�.");
        }

        // Program ba�lar ba�lamaz kameray� �stten ba�lat
        if (cameraZoom != null && robotTarget != null)
        {
            Vector3 startPosition = new Vector3(robotTarget.position.x, robotTarget.position.y + 10, robotTarget.position.z - 10);
            Quaternion startRotation = Quaternion.Euler(45, 0, 0); // Kameray� tepeden bakacak �ekilde ayarla
            cameraZoom.transform.position = startPosition;
            cameraZoom.transform.rotation = startRotation;
        }
    }

    void OnMouseDown()
    {
        if (cameraZoom != null && robotTarget != null)
        {
            Debug.Log("Robota t�kland�.");

            // Kamera hedefe biraz daha yak�n ama daha uzakta olacak �ekilde pozisyonu hesapla
            Vector3 targetPosition = robotTarget.position + new Vector3(0, 3, -8); // Yak�nla�t�rma mesafesi ve y�ksekli�i
            Quaternion targetRotation = Quaternion.LookRotation(robotTarget.position - targetPosition); // Robotu hedefle

            // Kamera yak�nla�t�rma i�lemini ba�lat
            cameraZoom.StartZooming(targetPosition, targetRotation, this);

            ShowAnaPanel();
        }
    }

    public void ShowAnaPanel()
    {
        if (AnaPanel != null)
        {
            Debug.Log("AnaPanel g�steriliyor.");
            AnaPanel.SetActive(true);

            Transform parent = AnaPanel.transform.parent;
            while (parent != null)
            {
                if (!parent.gameObject.activeSelf)
                {
                    Debug.LogWarning($"Parent {parent.gameObject.name} aktif de�il.");
                    parent.gameObject.SetActive(true);
                }
                parent = parent.parent;
            }

            // Ek hata ay�klama ad�mlar�
            if (AnaPanel.activeSelf)
            {
                Debug.Log("AnaPanel aktif durumda.");
            }
            else
            {
                Debug.LogWarning("AnaPanel aktif edilmesine ra�men hala pasif.");
            }
        }
        else
        {
            Debug.LogError("AnaPanel referans� atanm�� ancak GameObject bulunamad�.");
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
            Debug.LogError("AnaPanel referans� atanm�� ancak GameObject bulunamad�.");
        }
    }
}
