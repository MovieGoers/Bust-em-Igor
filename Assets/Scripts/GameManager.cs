using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject PlayerGameObject;

    float gameTimer;

    public int maxEnemyCount;
    public float enemySpawnTime;
    float enemySpawnTimer;

    bool isGamePaused;

    enum GameState { 
        playing,
        paused
    }

    GameState state;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        gameTimer = 0f;
        enemySpawnTimer = enemySpawnTime;
        state = GameState.playing;

        isGamePaused = false;

        StartCoroutine("StartGameTimer");
        StartCoroutine("StartEnemySpawnTimer");
    }

    IEnumerator StartGameTimer()
    {
        yield return null;
        int min = 0, sec = 0;
        while(state == GameState.playing)
        {
            min = (int)gameTimer / 60;
            sec = (int)gameTimer % 60;

            UIManager.Instance.SetGameTimerText(min, sec);
            yield return new WaitForSeconds(1f);
            gameTimer += 1;
        }
    }

    IEnumerator StartEnemySpawnTimer()
    {
        yield return null;
        while(state == GameState.playing)
        {
            if (enemySpawnTimer > 0)
            {
                enemySpawnTimer -= 0.05f;
            }

            if (enemySpawnTimer <= 0)
            {
                if (EnemyManager.Instance.GetEnemyCount() < maxEnemyCount)
                {
                    EnemyManager.Instance.SpawnNewSkeleton(100f + gameTimer, 0.01f + 0.001f * gameTimer, 0.01f + 0.001f * gameTimer, 100f + 10f * gameTimer);
                    enemySpawnTimer = enemySpawnTime;
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void PauseGame()
    {
        if(isGamePaused == false)
        {
            isGamePaused = true;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused == true)
        {
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }
}
