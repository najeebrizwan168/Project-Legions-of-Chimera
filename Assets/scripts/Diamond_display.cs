using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Diamond_display : MonoBehaviour
{
    public Text diamond_text;
    public Text Star_t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Update()
    {
        if (diamond_text != null)
        {
            diamond_text.text = PlayerPrefs.GetInt("diamond").ToString();
        }
       
        star();
    }
    public void star()
    {
        if (Star_t != null)
        {
            Star_t.text = PlayerPrefs.GetInt("Stars").ToString();
        }
    }


}
