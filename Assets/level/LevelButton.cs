using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelData levelData; // Assign in Inspector
   [ SerializeField ] AudioSource audioSource;
   
    public void OnLevelSelected()
    {
        audioSource.Play();
      
        LevelManager.SelectedLevel = levelData;
        Debug.Log(" " + levelData.levelIndex);
        LevelManager.loadin_bool= true;
        LevelManager.Current_scene = "level0";
        
       
      
    }
   
}
