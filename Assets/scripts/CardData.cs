// CardData.cs
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Data")]
public class CardData : ScriptableObject 
{
    public string cardID;
    public Sprite cardIcon;
    public GameObject characterPrefab;
    public int energyCost;
    public int health;
    public int Attack_damage;
    public List<GameObject> Fire_prefab;
    public GameObject DamageVfx_prefab;
    public List<RuntimeAnimatorController> animatorControllers;
    public int upgradelevel;
    public int level;
    public bool imageadjust;
    public AudioClip HeroAttackaudio;

    // Clone method
   
    public CardData Clone()
    {
        CardData clone = ScriptableObject.CreateInstance<CardData>();
        clone.cardID = cardID;
        clone.cardIcon = cardIcon;
        clone.characterPrefab = characterPrefab;
        clone.energyCost = energyCost;
        clone.health = health;
        clone.Attack_damage = Attack_damage;
        clone.Fire_prefab = new List<GameObject>(Fire_prefab);
        clone.DamageVfx_prefab = DamageVfx_prefab;
        clone.animatorControllers = animatorControllers;
        clone.level = level;
        clone.imageadjust = imageadjust;
        return clone;
    }
    public void LoadSavedData()
    {
        if (PlayerPrefs.HasKey(cardID + "health"))
            health = PlayerPrefs.GetInt(cardID + "health");
        if (PlayerPrefs.HasKey(cardID + "damage"))
            Attack_damage = PlayerPrefs.GetInt(cardID + "damage");
        if (PlayerPrefs.HasKey(cardID + "energy"))
            energyCost = PlayerPrefs.GetInt(cardID + "energy");
    }

}
