using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelMapViewSettings:MonoBehaviour
{
    public GameObject levelButtonPrefab;
    public Transform levelButtonsContainer;
    public Button backButton;
    public Canvas mapCanvas;

    // OLD GRID SETTINGS (ignored now, but you can keep them)
    public float buttonSpacing = 100f;
    public int buttonsPerRow = 5;

    // ✅ NEW: Points for custom placement
    public Transform[] levelPoints; // Assign 10 empty objects here
}
