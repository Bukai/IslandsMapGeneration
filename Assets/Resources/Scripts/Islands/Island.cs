using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Reward
{
    public Sprite icon;
    public int quantity;
}

[System.Serializable]
public class IslandInfo
{
    public string islandName;
    public Sprite islandSprite;
    public List<Reward> rewards = new List<Reward>();
    public string difficultyText;
}

public class Island : MonoBehaviour
{
    private IslandInfo islandInfo = new IslandInfo();
    private SpriteRenderer spriteRenderer;
    private string[] difficultyLevels = { "Easy", "Medium", "Hard" };

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        AssignRandomSprite();
        AssignRandomName();
        AssignRandomDifficulty();
        GenerateRandomRewards();
    }

    private void AssignRandomSprite()
    {
        var sprites = AssetManager.Instance.islandSprites;
        if (sprites != null && sprites.Count > 0)
        {
            islandInfo.islandSprite = sprites[Random.Range(0, sprites.Count)];
            spriteRenderer.sprite = islandInfo.islandSprite;
        }
    }

    private void AssignRandomName()
    {
        string[] adjectives = { "Mystic", "Emerald", "Lost", "Stormy", "Golden", "Silent", "Ancient" };
        string[] geographicNames = { "Cove", "Isle", "Lagoon", "Point", "Bay", "Rock", "Haven" };

        string randomAdjective = adjectives[Random.Range(0, adjectives.Length)];
        string randomGeographic = geographicNames[Random.Range(0, geographicNames.Length)];

        islandInfo.islandName = $"{randomAdjective} {randomGeographic}";
    }

    private void AssignRandomDifficulty()
    {
        islandInfo.difficultyText = difficultyLevels[Random.Range(0, difficultyLevels.Length)];
    }

    private void GenerateRandomRewards()
    {
        var icons = AssetManager.Instance.rewardIcons;
        int rewardCount = Random.Range(2, 5);
        for (int i = 0; i < rewardCount; i++)
        {
            Reward reward = new Reward
            {
                icon = icons[Random.Range(0, icons.Count)],
                quantity = Random.Range(1, 16)
            };
            islandInfo.rewards.Add(reward);
        }
    }
}
