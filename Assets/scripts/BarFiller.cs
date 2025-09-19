using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BarFiller : MonoBehaviour
{
    public RectTransform barTransform;
    public GameObject dotPrefab;
    public int totalDots = 15;
    public float spawnInterval = 2f;
    public float sideMargin = 20f;
    public Animator bar_animator;
    public Text level_text;
    public int EXP;

    public List<GameObject> energyDots = new List<GameObject>();  // Store dots

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private Coroutine fillCoroutine; // 👈 Track the coroutine
    public TextMeshProUGUI exp;
    void Start()
    {
        float barWidth = barTransform.rect.width;
        float usableWidth = barWidth - (2 * sideMargin);

        Vector3 barCenter = barTransform.position;
        Vector3 barRight = barTransform.right;
        leftEdge = barCenter - barRight * (usableWidth / 2f);
        rightEdge = barCenter + barRight * (usableWidth / 2f);

        fillCoroutine = StartCoroutine(FillBar());
        level_text.text = "Level "+(LevelManager.SelectedLevel.levelIndex+1).ToString(); 
    }

    private IEnumerator FillBar()
    {
        while (energyDots.Count < totalDots)
        {
            float t = (float)energyDots.Count / (totalDots - 1);
            Vector3 spawnPos = Vector3.Lerp(leftEdge, rightEdge, t);
            GameObject dot = Instantiate(dotPrefab, spawnPos, Quaternion.identity, barTransform);
            energyDots.Add(dot);
            yield return new WaitForSeconds(spawnInterval);
        }

        fillCoroutine = null; // 👈 Reset once done
    }

    public void ConsumeDots(int count)
    {
        for (int i = 0; i < count && energyDots.Count > 0; i++)
        {
            int lastIndex = energyDots.Count - 1;
            Destroy(energyDots[lastIndex]);
            energyDots.RemoveAt(lastIndex);
        }

        // Only start filling if not already running
        if (energyDots.Count < totalDots && fillCoroutine == null)
        {
            fillCoroutine = StartCoroutine(FillBar());
        }
    }

    public bool HasEnoughDots(int required)
    {

        if (required > energyDots.Count)
        {
            bar_animator.SetTrigger("BarTrigger");
        }
       

        return energyDots.Count >= required;
    }
    public void InstantFillBar()
    {
        if (GameManager.Inmatchscore >= EXP)
        {
            if (fillCoroutine != null)
            {
                StopCoroutine(fillCoroutine);
                fillCoroutine = null;
            }

            // Destroy existing dots if needed (optional)
            while (energyDots.Count < totalDots)
            {
                float t = (float)energyDots.Count / (totalDots - 1);
                Vector3 spawnPos = Vector3.Lerp(leftEdge, rightEdge, t);
                GameObject dot = Instantiate(dotPrefab, spawnPos, Quaternion.identity, barTransform);
                energyDots.Add(dot);
            }
            GameManager.expuse(EXP);


        }
        else
        {
            GameManager.Instance.insuffient();

        }
    }
        // Stop coroutine if it's running
     
    
}
