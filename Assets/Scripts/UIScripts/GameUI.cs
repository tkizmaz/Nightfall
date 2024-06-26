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

    public void ChangePotionCount(StatType statType, int potionCount)
    {
        TMPro.TMP_Text textToChange = statType == StatType.Health ? healtPotionCountText : manaPotionCountText;
        textToChange.text = potionCount.ToString();
    }

    public void ChangeStatText(StatType statType, int statValue)
    {
        TMPro.TMP_Text textToChange = statType == StatType.Health ? healthText : manaText;
        textToChange.text = statType.ToString() + ": " + statValue.ToString();
    }

    public void ChangeAbilityStatus(AbilityType abilityType, bool status, bool isSelected)
    {
        Image imageToChange = abilityType == AbilityType.Blink ? blinkIcon : bendTimeIcon;
        imageToChange.color = (status && isSelected) ? Color.green : Color.grey;
    }

    public void ChangeAbilitySelection(AbilityType abilityType, bool isSelected)
    {
        Color color = isSelected ? Color.green : Color.white;
        Image imageToChange = abilityType == AbilityType.Blink ? blinkIcon : bendTimeIcon;
        imageToChange.color = color;
    }

    private void Start() 
    {
        healtPotionCountText.text = GameManager.instance.initialPotionCount.ToString();
        manaPotionCountText.text = GameManager.instance.initialPotionCount.ToString();
    }
}
