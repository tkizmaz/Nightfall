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
    private GameObject player;
    [SerializeField]
    private Image blinkIcon;

    public void ChangeManaText(int mana)
    {
        manaText.text = "Mana: " + mana;
    }

    public void ChangeAbilityStatus(bool status)
    {
        blinkIcon.color = status ? Color.white : Color.grey;
    }
}
