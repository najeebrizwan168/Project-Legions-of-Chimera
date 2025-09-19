using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitManager : MonoBehaviour
{
    public static PlayerUnitManager Instance;
    public List<Transform> activePlayers = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        activePlayers.Clear();

        foreach (GameObject player in players)
        {
            activePlayers.Add(player.transform);
            
        }
    }

   
}
