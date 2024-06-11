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

    // Start is called before the first frame update
    void Start()
    {
    }
}
