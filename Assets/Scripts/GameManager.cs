using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject PlayerGameObject;

    public int maxEnemyCount;
    public float enemySpawnTime;
    float enemySpawnTimer;

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
        enemySpawnTimer = enemySpawnTime;
    }

    private void Update()
    {
        if(enemySpawnTimer > 0)
        {
            enemySpawnTimer -= Time.deltaTime;
        }

        if(enemySpawnTimer <= 0)
        {
            if(EnemyManager.Instance.GetEnemyCount() < maxEnemyCount)
            {
                EnemyManager.Instance.SpawnNewSkeleton(100f, 0.01f, 0.01f);
                enemySpawnTimer = enemySpawnTime;
            }
        }
    }
}
