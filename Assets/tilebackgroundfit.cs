using UnityEngine;

[ExecuteAlways] // Works in editor + play mode
[RequireComponent(typeof(SpriteRenderer))]
public class TileBackgroundFit : MonoBehaviour
{
    [Header("Sprite Setup")]
    public Sprite sprite;                  // assign sprite
    public float pixelsPerUnit = 100f;     // usually 100 (check sprite import settings)

    [Header("Scale Options")]
    public bool scaleWidth = true;
    public bool scaleHeight = true;

    private SpriteRenderer sr;
    private Camera mainCam;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
        ApplySettings();
    }

    void Update()
    {
        ApplySettings();
    }

    void ApplySettings()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (sprite != null) sr.sprite = sprite;

        sr.drawMode = SpriteDrawMode.Tiled;

        if (mainCam == null)
            mainCam = Camera.main;

        if (mainCam == null) return;

        // ✅ Calculate world screen size based on camera
        float worldHeight = mainCam.orthographicSize * 2f;
        float worldWidth = worldHeight * mainCam.aspect;

        // ✅ Start with current size (so non-scaled axis is preserved)
        Vector2 newSize = sr.size;

        if (scaleWidth) newSize.x = worldWidth;
        if (scaleHeight) newSize.y = worldHeight;

        sr.size = newSize;
    }
}
