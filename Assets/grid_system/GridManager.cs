using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public int rows = 4;
    public int columns = 5;
    public Vector2 cellSize = new Vector2(100, 100);
    public Vector2 spacing = new Vector2(10, 10);

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        RectTransform rt = GetComponent<RectTransform>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                RectTransform cellRT = cell.GetComponent<RectTransform>();
                cellRT.sizeDelta = cellSize;

                float posX = col * (cellSize.x + spacing.x);
                float posY = -row * (cellSize.y + spacing.y);
                cellRT.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }
}

