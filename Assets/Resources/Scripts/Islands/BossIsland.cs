using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BossReward
{
    public Sprite icon;
    public int quantity;
}

[System.Serializable]
public class BossIslandInfo
{
    public string islandName = "Doom Citadel";
    public Sprite islandSprite;
    public List<BossReward> rewards = new List<BossReward>();
    public string difficultyText = "Hard";
}

public class BossIsland : MonoBehaviour
{
    private BossIslandInfo bossIslandInfo = new BossIslandInfo();
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        AssignBossSprite();
        GenerateBossRewards();
    }

    private void OnMouseDown()
    {
        BossIslandUIManager.Instance.ShowBossInfo(bossIslandInfo);
    }

    private void AssignBossSprite()
    {
        bossIslandInfo.islandSprite = AssetManager.Instance.bossIslandSprite;
        spriteRenderer.sprite = bossIslandInfo.islandSprite;
    }

    private void GenerateBossRewards()
    {
        var icons = AssetManager.Instance.rewardIcons;
        int rewardCount = 3;

        for (int i = 0; i < rewardCount; i++)
        {
            BossReward reward = new BossReward
            {
                icon = icons[Random.Range(0, icons.Count)],
                quantity = Random.Range(10, 26)
            };
            bossIslandInfo.rewards.Add(reward);
        }
    }
}
