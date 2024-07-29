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
    private TMPro.TMP_Text countdownText;
    [SerializeField]
    private TMPro.TMP_Text remainingEnemiesText;
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
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private Slider sensitivitySlider;
    private bool isSettingsPanelActive;
    private CameraController cameraController;

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
        SetSensitivitySlider();
        healthSlider.fillAmount = 1;
        manaSlider.fillAmount = 1;
        healtPotionCountText.text = GameManager.instance.initialPotionCount.ToString();
        manaPotionCountText.text = GameManager.instance.initialPotionCount.ToString();
        abilityIconDict.Add(AbilityType.Blink, blinkIcon);
        abilityIconDict.Add(AbilityType.BendTime, bendTimeIcon);
        abilityIconDict.Add(AbilityType.DarkVision, darkVisionIcon);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isSettingsPanelActive = !isSettingsPanelActive;
            settingsPanel.SetActive(isSettingsPanelActive);
            if(isSettingsPanelActive)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void SetSensitivitySlider()
    {
        cameraController = player.GetComponentInChildren<CameraController>();
        sensitivitySlider.value = cameraController.sensitivity;
        settingsPanel.SetActive(false);
    }

    public void ChangeSensitivity()
    {
        cameraController.sensitivity = sensitivitySlider.value;
    }

    public void UpdateTimerText(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateRemainingEnemiesText(int remainingEnemies)
    {
        remainingEnemiesText.text = "Remaining Enemies: " + remainingEnemies.ToString();
    }

    public void ActivateGameOverPanel(bool isWin)
    {
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        TMPro.TMP_Text gameOverText = gameOverPanel.GetComponentInChildren<TMPro.TMP_Text>();
        gameOverText.text = isWin ? "You Win!" : "You Lose!";

        TMPro.TMP_Text gameOverButtonText = gameOverPanel.GetComponentsInChildren<TMPro.TMP_Text>()[1];
        gameOverButtonText.text = isWin ? "Play Again" : "Retry";
    }

}
