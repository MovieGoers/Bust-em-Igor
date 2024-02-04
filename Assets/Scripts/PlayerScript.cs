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
        targetEnemy = FindNearestEnemy(); // ����ȭ�� ���� Update �Լ� ��� �ٸ� ������ Ư�� ��Ȳ �Ǹ� ȣ��ǵ��� ���� �ʿ�.

        moveDirection = (targetEnemy.transform.position - transform.position).normalized;

        if(!isTargetNear)
            transform.Translate(moveDirection * speed);
    }

    private void Update()
    {
        UIManager.Instance.SetAttackText(attackTimer);

        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer <= 0)
        {
            if (isTargetNear) {
                isTargetNear = false;
                Attack(targetEnemy);
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

    void Attack(GameObject enemy)
    {
        enemy.GetComponent<SkeletonScript>().hp -= damage;
        if(enemy.GetComponent<SkeletonScript>().hp <= 0)
        {
            Destroy(enemy);
            EnemyManager.Instance.RemoveSkeletonFromList(enemy);
        }
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
