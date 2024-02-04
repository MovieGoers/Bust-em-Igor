using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float damage;
    public float speed;
    public float hp;
    public float attackTime;
    float attackTimer;

    bool isTargetNear;

    Vector3 moveDirection;
    GameObject targetEnemy;

    private void Start()
    {
        attackTimer = attackTime;
        isTargetNear = false;
    }

    private void FixedUpdate()
    {
        targetEnemy = FindNearestEnemy(); // 최적화를 위해 Update 함수 대신 다른 곳에서 특정 상황 되면 호출되도록 구현 필요.

        moveDirection = (targetEnemy.transform.position - transform.position).normalized;
        
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        if (!isTargetNear)
            transform.position += moveDirection * speed;
    }

    private void Update()
    {
        UIManager.Instance.SetAttackText(attackTimer);
        UIManager.Instance.SetHPText(hp);

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer <= 0)
        {
            if (isTargetNear) {
                AttackEnemy(targetEnemy);
                attackTimer = attackTime;
            }
        }
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

    void AttackEnemy(GameObject enemy)
    {
        enemy.GetComponent<SkeletonScript>().hp -= damage;
        if(enemy.GetComponent<SkeletonScript>().hp <= 0)
        {
            Destroy(enemy);
            EnemyManager.Instance.RemoveSkeletonFromList(enemy);
            isTargetNear = false;
        }
    }

    public void AttackPlayer(float damage)
    {
        hp -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Enemy"))
        {
            isTargetNear = true;
        }
    }
}
