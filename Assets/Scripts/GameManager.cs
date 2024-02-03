using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

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

    public GameObject PlayerGameObject;

    private void Start()
    {
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(5f, 5f, 5f));
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(-5f, 5f, 5f));
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(5f, 5f, -5f));
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(-5f, 5f, -5f));
    }
}
