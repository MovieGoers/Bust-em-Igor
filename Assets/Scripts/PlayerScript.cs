using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float damage;
    public float speed;
    public float hp;
    public float attackRate;

    Vector3 moveDirection;
    GameObject targetEnemy;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject targetEnemy = FindNearestEnemy();

        moveDirection = (targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * speed);
    }

    GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float minimumDistance = 9999f;
        foreach (GameObject enemy in EnemyManager.Instance.skeletons)
        {
            float tempDistance = (transform.position - enemy.transform.position).magnitude;
            if(tempDistance < minimumDistance)
            {
                minimumDistance = tempDistance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
            return nearestEnemy;
        
        return null;
    }

    void Attack(GameObject enemy)
    {
        enemy.GetComponent<SkeletonScript>().hp -= damage;
        if(enemy.GetComponent<SkeletonScript>().hp <= 0)
        {
            Destroy(enemy);
            EnemyManager.Instance.RemoveSkeletonFromList(enemy);
            targetEnemy = FindNearestEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Enemy"))
        {
            Attack(go);
        }
    }
}
