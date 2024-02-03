using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject PlayerGameObject;

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
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(2f, 5f, 2f));
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(-3f, 5f, 3f));
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(4f, 5f, -4f));
        EnemyManager.Instance.SpawnNewSkeleton(new Vector3(-5f, 5f, -5f));
    }
}
