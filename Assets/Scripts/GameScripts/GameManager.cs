using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameObject player;    
    public int initialPotionCount = 3;
    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> EnemyList { get => enemyList ;}
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        CheckForReopenTheScene();
    }

    void CheckForReopenTheScene()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
