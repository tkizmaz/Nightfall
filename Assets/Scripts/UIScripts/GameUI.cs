using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text healthText;
    [SerializeField]
    private TMPro.TMP_Text manaText;
    [SerializeField]
    private GameObject player;

    public void ChangeManaText(int mana)
    {
        manaText.text = "Mana: " + mana;
    }
}
