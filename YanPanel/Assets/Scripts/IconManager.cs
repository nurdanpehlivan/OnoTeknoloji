using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class IconManager : MonoBehaviour
{
    public List<Image> icons; 
    public Image line; 
    public float animationDuration = 0.5f; 

    private RectTransform lineRectTransform;
    private Image selectedIcon;

    void Start()
    {
        lineRectTransform = line.GetComponent<RectTransform>();
        UpdateIconList();
        HideLine();
    }

    void UpdateIconList()
    {
        foreach (Image icon in icons)
        {
            Button button = icon.GetComponent<Button>();

            if (button == null)
            {
                // E�er Button bile�eni yoksa, yeni bir tane ekle
                button = icon.gameObject.AddComponent<Button>();
            }

            // Eski dinleyicileri kald�r ve yeni dinleyici ekle
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Select(icon));
        }
    }

    void ShowLine(Image icon)
    {
        if (line != null)
        {
            RectTransform iconRectTransform = icon.GetComponent<RectTransform>();
            if (iconRectTransform != null)
            {
                // �izginin yeni konumunu hesapla
                Vector2 iconSize = iconRectTransform.rect.size;
                Vector2 lineSize = lineRectTransform.rect.size;

                // �izginin ikonun merkezine hizalanacak �ekilde konumland�r�lmas�
                Vector2 targetPosition = iconRectTransform.anchoredPosition;
                targetPosition += new Vector2(0, iconSize.y / 2 - lineSize.y / 2);

                // �izginin geni�li�ini ikonun geni�li�iyle e�itle
                lineRectTransform.sizeDelta = new Vector2(iconSize.x, lineSize.y);

                // Animasyon ba�lat
                StartCoroutine(MoveLine(targetPosition));
            }
            else
            {
                Debug.LogError("Icon'un RectTransform bile�eni eksik.");
            }
        }
    }
    void HideLine()
    {
        if (line != null)
        {
            line.gameObject.SetActive(false);
        }
    }

    public void Select(Image icon)
    {
        if (icon == null)
        {
            Debug.LogError("Se�ilen ikon referans� eksik.");
            return;
        }

        if (selectedIcon != icon)
        {
            HideLine(); 
            ShowLine(icon); 
            selectedIcon = icon; 
        }
    }

    IEnumerator MoveLine(Vector2 targetPosition)
    {
        if (line == null)
            yield break;

        line.gameObject.SetActive(true);

        Vector2 startPosition = lineRectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            // Zaman� normalize et
            float t = elapsedTime / animationDuration;
            // Yumu�ak ge�i� i�in Lerp kullan
            lineRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lineRectTransform.anchoredPosition = targetPosition;
    }
}
