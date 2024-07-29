using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameObject player;    
    public int initialPotionCount = 3;
    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> EnemyList { get => enemyList ;}
    [SerializeField]
    private float timeLimit = 300f;
    private float timer;
    private GameUI gameUI;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            timer = timeLimit;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemyList.Remove(enemy);
        gameUI.UpdateRemainingEnemiesText(enemyList.Count);
        CheckAllEnemiesDefeated();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            gameUI.UpdateTimerText(timer);

            if (timer <= 0)
            {
                CheckAllEnemiesDefeated();
            }
        }
    }

    private void CheckAllEnemiesDefeated()
    {
        if(enemyList.Count == 0)
        {
            gameUI.ActivateGameOverPanel(true);
        }
        else
        {   
            if(timer <= 0)
            gameUI.ActivateGameOverPanel(false);
        }
    }

    private void Start() 
    {
        gameUI = this.gameObject.GetComponent<GameUI>();
        gameUI.UpdateRemainingEnemiesText(enemyList.Count);
    }

    public void PlayNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Player Died");
        gameUI.ActivateGameOverPanel(false);
    }
}
