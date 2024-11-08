using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class IslandUIManager : MonoBehaviour
{
    public static IslandUIManager Instance;

    [SerializeField] private GameObject islandInfoPanel;
    [SerializeField] private TextMeshProUGUI islandNameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private List<Image> rewardIcons;
    [SerializeField] private List<TextMeshProUGUI> rewardTexts;

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
        islandInfoPanel.SetActive(false);
    }

    public void ShowIslandInfo(IslandInfo islandInfo)
    {
        islandNameText.text = islandInfo.islandName;
        difficultyText.text = islandInfo.difficultyText;

        for (int i = 0; i < rewardIcons.Count; i++)
        {
            if (i < islandInfo.rewards.Count)
            {
                rewardIcons[i].sprite = islandInfo.rewards[i].icon;
                rewardTexts[i].text = islandInfo.rewards[i].quantity.ToString();
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

        islandInfoPanel.SetActive(true);
    }

    public void HideUI()
    {
        islandInfoPanel.SetActive(false);
    }
}
