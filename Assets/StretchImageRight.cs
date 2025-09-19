using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways] // works in edit mode & play mode
public class StretchImageRight : MonoBehaviour
{
    [Header("Assign your UI Image here")]
    public Image targetImage;

    [Header("Scaling Value")]
    [Range(0.001f, 0.97f)]
    public float scaleX = 1f;

    private RectTransform rectTransform;

    void Start()
    {
        if (targetImage != null)
        {
            rectTransform = targetImage.rectTransform;

            // Important: Anchor left side
            rectTransform.anchorMin = new Vector2(0, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(0, rectTransform.anchorMax.y);
            rectTransform.pivot = new Vector2(0, 0.5f); // pivot at left center
        }
    }

    void Update()
    {
        if (rectTransform != null)
        {
            float sizebar = PlayerPrefs.GetInt("Stars") * 0.0161f;
            scaleX = sizebar;
            Vector2 size = rectTransform.sizeDelta;
            size.x = scaleX * 100f; // base width (100) * scale
            rectTransform.sizeDelta = size;
        }
    }
}
