using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperText : MonoBehaviour
{
    public string shoptext;
    public TextMeshProUGUI Text;
    
    public void textWrite()
    {
        Text.text = shoptext;
    }

}
