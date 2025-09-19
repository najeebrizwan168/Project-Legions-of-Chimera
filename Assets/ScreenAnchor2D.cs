using UnityEngine;

[ExecuteAlways] // Works in editor + play mode
public class ScreenAnchor2D : MonoBehaviour
{
    [Header("Anchor Options")]
    public bool anchorLeft;
    public bool anchorRight;
    public bool anchorTop;
    public bool anchorBottom;

    public float offsetX = 0f; // extra padding
    public float offsetY = 0f;

    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        ApplyAnchor();
    }

    void Update()
    {
        ApplyAnchor();
    }

    void ApplyAnchor()
    {
        if (mainCam == null)
            mainCam = Camera.main;

        if (mainCam == null) return;

        // World screen bounds
        float worldHeight = mainCam.orthographicSize * 2f;
        float worldWidth = worldHeight * mainCam.aspect;

        Vector3 newPos = transform.position;

        // Horizontal anchoring
        if (anchorLeft) newPos.x = mainCam.transform.position.x - worldWidth / 2f + offsetX;
        if (anchorRight) newPos.x = mainCam.transform.position.x + worldWidth / 2f - offsetX;

        // Vertical anchoring
        if (anchorBottom) newPos.y = mainCam.transform.position.y - worldHeight / 2f + offsetY;
        if (anchorTop) newPos.y = mainCam.transform.position.y + worldHeight / 2f - offsetY;

        transform.position = newPos;
    }
}
