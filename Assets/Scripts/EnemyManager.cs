using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    GameObject player;

    public GameObject originalSkeleton;
    public List<GameObject> skeletons = new List<GameObject>();

    public float spawnDistance;

    int enemyCount;

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

    private void Start()
    {
        player = GameManager.Instance.PlayerGameObject;
        enemyCount = 0;
    }

    public void SpawnNewSkeleton(float hp, float damage, float speed, float exp)
    {
        GameObject newSkeleton = Instantiate(originalSkeleton);

        newSkeleton.SetActive(true);
        newSkeleton.GetComponent<SkeletonScript>().hp = hp;
        newSkeleton.GetComponent<SkeletonScript>().damage = damage;
        newSkeleton.GetComponent<SkeletonScript>().speed = speed;
        newSkeleton.GetComponent<SkeletonScript>().exp = exp;

        float randomAngle = Random.Range(0f, 360f);

        float x = Mathf.Cos(randomAngle) * spawnDistance + player.transform.position.x;
        float z = Mathf.Sin(randomAngle) * spawnDistance + player.transform.position.z;

        newSkeleton.transform.position = new Vector3(x, newSkeleton.transform.position.y, z);

        skeletons.Add(newSkeleton);

        enemyCount++;
    }

    public void RemoveSkeletonFromList(GameObject skeleton)
    {
        skeletons.Remove(skeleton);
        enemyCount--;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
}
