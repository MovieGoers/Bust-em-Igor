using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    public GameObject originalSkeleton;
    List<GameObject> skeletons = new List<GameObject>();

    public static EnemyManager Instance
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

    public void SpawnNewSkeleton(Vector3 pos)
    {
        GameObject newSkeleton = Instantiate(originalSkeleton);

        newSkeleton.SetActive(true);
        newSkeleton.transform.position = pos;

        skeletons.Add(newSkeleton);
    }
}
