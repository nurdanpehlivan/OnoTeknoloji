using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorManager : MonoBehaviour
{
    public Image background; 

    private bool isOriginalColor = true;
    public Color originalColor = Color.white;
    public Color newColor = Color.gray;

    void Start()
    {
        if (background == null)
        {
            Debug.LogError("Background Image is not assigned.");
        }
        else
        {
            background.color = originalColor; // Baþlangýç rengini ayarla
        }
    }

    public void ToggleBackgroundColor()
    {
        if (background != null)
        {
            if (isOriginalColor)
            {
                background.color = newColor;
            }
            else
            {
                background.color = originalColor;
            }
            isOriginalColor = !isOriginalColor;
        }
    }
}
