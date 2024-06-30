using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text healthText;
    [SerializeField]
    private TMPro.TMP_Text manaText;
    [SerializeField]
    private TMPro.TMP_Text healtPotionCountText;
    [SerializeField]
    private TMPro.TMP_Text manaPotionCountText;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Image blinkIcon;
    [SerializeField]
    private Image bendTimeIcon;
    [SerializeField]
    private Image darkVisionIcon;
    [SerializeField]
    private Image healthSlider;
    [SerializeField]
    private Image manaSlider;
    private Dictionary<AbilityType, Image> abilityIconDict = new Dictionary<AbilityType, Image>();

    public void ChangePotionCount(StatType statType, int potionCount)
    {
        TMPro.TMP_Text textToChange = statType == StatType.Health ? healtPotionCountText : manaPotionCountText;
        textToChange.text = potionCount.ToString();
    }

    public void ChangeStatText(StatType statType, int statValue)
    {
        Image sliderToChange = statType == StatType.Health ? healthSlider : manaSlider;
        sliderToChange.fillAmount = statValue / 100f;
    }

    public void ChangeAbilityStatus(AbilityType abilityType, bool status, bool isSelected)
    {
        Image imageToChange = abilityIconDict[abilityType];
        imageToChange.color = (status && isSelected) ? Color.green : Color.grey;
    }

    public void ChangeAbilitySelection(AbilityType abilityType, bool isSelected)
    {
        Image imageToChange = abilityIconDict[abilityType];
        Color color = isSelected ? Color.green : Color.white;
        imageToChange.color = color;
    }

    private void Start() 
    {
        healthSlider.fillAmount = 1;
        manaSlider.fillAmount = 1;
        healtPotionCountText.text = GameManager.instance.initialPotionCount.ToString();
        manaPotionCountText.text = GameManager.instance.initialPotionCount.ToString();
        abilityIconDict.Add(AbilityType.Blink, blinkIcon);
        abilityIconDict.Add(AbilityType.BendTime, bendTimeIcon);
        abilityIconDict.Add(AbilityType.DarkVision, darkVisionIcon);
    }
}
