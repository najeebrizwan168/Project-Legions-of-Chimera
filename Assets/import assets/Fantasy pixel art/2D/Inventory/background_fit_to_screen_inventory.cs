using UnityEngine;

public class background_fit_to_screen_inventory : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null || sr.sprite == null)
        {
            Debug.LogError("Missing SpriteRenderer or Sprite!");
            return;
        }

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        transform.localScale = new Vector3(
            worldScreenWidth / spriteSize.x,
            worldScreenHeight / spriteSize.y,
            1);
    }
}

