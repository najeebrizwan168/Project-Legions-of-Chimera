using Unity.VisualScripting;
using UnityEngine;

public class TileReactivator : MonoBehaviour
{
    public int EXP = 0;
    public void EnableAllDisabledTiles()
    {
        if (GameManager.Inmatchscore >= EXP)
        {

            GameObject grid = GameObject.FindGameObjectWithTag("Grid");

            if (grid == null)
            {
                Debug.LogError("No GameObject found with tag 'Grid'!");
                return;
            }

            foreach (Transform child in grid.transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
            }

            Debug.Log("All disabled tiles reactivated.");
            GameManager.expuse(EXP);
            Debug.Log(" "+GameManager.Inmatchscore);
           
        }
        else
        {
            GameManager.Instance.insuffient();
        }


    }
}
