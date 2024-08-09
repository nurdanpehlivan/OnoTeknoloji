using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class IconManager : MonoBehaviour
{
    public List<Image> icons;
    public List<GameObject> sekmePanels; // Sekme panelleri
    public Image line;
    public float animationDuration = 0.5f;
    public float panelWidth = 600f; // Panellerin geniþliði

    private RectTransform lineRectTransform;
    private Image selectedIcon;
    private GameObject selectedSekmePanel;

    void Start()
    {
        lineRectTransform = line.GetComponent<RectTransform>();
        UpdateIconList();
        HideLine();
        HideAllSekmePanels();
    }

    void UpdateIconList()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            Image icon = icons[i];
            Button button = icon.GetComponent<Button>();

            if (button == null)
            {
                button = icon.gameObject.AddComponent<Button>();
            }

            int index = i; // Lambda ifadesinde kullanýlmak üzere yerel bir kopya
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Select(icon, index));
        }
    }

    void ShowLine(Image icon)
    {
        if (line != null)
        {
            RectTransform iconRectTransform = icon.GetComponent<RectTransform>();
            if (iconRectTransform != null)
            {
                Vector2 iconSize = iconRectTransform.rect.size;
                Vector2 lineSize = lineRectTransform.rect.size;

                Vector2 targetLinePosition = iconRectTransform.anchoredPosition;
                targetLinePosition += new Vector2(0, iconSize.y / 2 - lineSize.y / 2);

                lineRectTransform.sizeDelta = new Vector2(iconSize.x, lineSize.y);

                StartCoroutine(MoveLine(targetLinePosition));
                line.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Icon'un RectTransform bileþeni eksik.");
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

    void HideAllSekmePanels()
    {
        foreach (GameObject panel in sekmePanels)
        {
            if (panel != null)
            {
                RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
                if (panelRectTransform != null)
                {
                    StartCoroutine(SlidePanel(panelRectTransform, new Vector2(-panelWidth, 0), animationDuration)); // Sol tarafa kaydýrarak gizle
                }
            }
        }
    }

    void ShowSekmePanel(GameObject panel)
    {
        if (panel != null)
        {
            RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
            if (panelRectTransform != null)
            {
                panel.SetActive(true);
                panelRectTransform.anchoredPosition = new Vector2(-panelWidth, 0); // Baþlangýçta sol tarafa kaydýrýlmýþ
                StartCoroutine(SlidePanel(panelRectTransform, Vector2.zero, animationDuration)); // Sað tarafa kaydýrarak göster
            }
        }
    }

    IEnumerator SlidePanel(RectTransform panelRectTransform, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = panelRectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            panelRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelRectTransform.anchoredPosition = targetPosition;
    }

    public void Select(Image icon, int index)
    {
        if (icon == null || index < 0 || index >= sekmePanels.Count)
        {
            Debug.LogError("Geçersiz ikon referansý veya panel dizini.");
            return;
        }

        if (selectedIcon == icon)
        {
            // Ayný ikona týklanýyorsa, sekme panelini aç veya kapat
            if (selectedSekmePanel != null)
            {
                bool isActive = selectedSekmePanel.activeSelf;
                if (isActive)
                {
                    // Paneli gizle
                    HideAllSekmePanels();
                    HideLine(); // Line'ý gizle
                    selectedIcon = null; // Seçili ikonu sýfýrla
                    selectedSekmePanel = null; // Seçili paneli sýfýrla
                }
                else
                {
                    // Paneli göster
                    ShowSekmePanel(selectedSekmePanel);
                    ShowLine(selectedIcon); // Line'ý göster
                }
            }
        }
        else
        {
            // Yeni bir ikon seçildiyse eski ikonu gizle ve yeni ikonu seç
            HideLine();
            ShowLine(icon);
            selectedIcon = icon;

            // Önceki sekme panelini gizle
            HideAllSekmePanels();
            // Yeni sekme panelini göster
            selectedSekmePanel = sekmePanels[index];
            ShowSekmePanel(selectedSekmePanel);
        }
    }

    IEnumerator MoveLine(Vector2 targetLinePosition)
    {
        if (line == null)
            yield break;

        Vector2 startLinePosition = lineRectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            lineRectTransform.anchoredPosition = Vector2.Lerp(startLinePosition, targetLinePosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lineRectTransform.anchoredPosition = targetLinePosition;
    }
}
