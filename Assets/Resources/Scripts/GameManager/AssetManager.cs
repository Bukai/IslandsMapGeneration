using UnityEngine;
using System.Collections.Generic;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public List<Sprite> islandSprites;
    public List<Sprite> rewardIcons;
    public Sprite bossIslandSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
