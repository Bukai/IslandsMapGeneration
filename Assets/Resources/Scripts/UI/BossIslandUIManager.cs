using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BossIslandUIManager : MonoBehaviour
{
    public static BossIslandUIManager Instance;

    [SerializeField] private GameObject bossInfoPanel;
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private List<TextMeshProUGUI> rewardTexts;
    [SerializeField] private List<Image> rewardIcons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bossInfoPanel.SetActive(false);
    }

    public void ShowBossInfo(BossIslandInfo bossIslandInfo)
    {
        bossNameText.text = bossIslandInfo.islandName;
        difficultyText.text = bossIslandInfo.difficultyText;

        for (int i = 0; i < rewardTexts.Count; i++)
        {
            if (i < bossIslandInfo.rewards.Count)
            {
                rewardIcons[i].sprite = bossIslandInfo.rewards[i].icon;
                rewardTexts[i].text = bossIslandInfo.rewards[i].quantity.ToString();
                rewardIcons[i].gameObject.SetActive(true);
                rewardTexts[i].gameObject.SetActive(true);
            }
            else
            {
                rewardIcons[i].sprite = null;
                rewardTexts[i].text = "";
                rewardIcons[i].gameObject.SetActive(false);
                rewardTexts[i].gameObject.SetActive(false);
            }
        }

        bossInfoPanel.SetActive(true);
    }

    public void HideUI()
    {
        bossInfoPanel.SetActive(false);
    }
}
