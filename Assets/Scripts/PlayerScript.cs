using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float damage;
    public float speed;
    public float hp;
    public float attackTime;
    public float attackRange;
    float attackTimer;

    Vector3 moveDirection;
    GameObject targetEnemy;

    Animator animator;

    enum State {
        idle,
        moving,
        attacking
    }

    State state;

    private void Start()
    {
        attackTimer = attackTime;
        state = State.idle;

        animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        targetEnemy = FindNearestEnemy(); // 최적화를 위해 Update 함수 대신 다른 곳에서 특정 상황 되면 호출되도록 구현 필요.

        HandleState();

        moveDirection = (targetEnemy.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        if (state == State.moving)
        {
            transform.position = new Vector3(transform.position.x + moveDirection.x * speed, transform.position.y, transform.position.z + moveDirection.z * speed);
        }
        
    }

    private void Update()
    {
        UIManager.Instance.SetAttackText(attackTimer);
        UIManager.Instance.SetHPText(hp);

        animator.SetInteger("State", (int)state);

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer <= 0)
        {
            if (state == State.attacking) {
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
        }
    }

    public void AttackPlayer(float damage)
    {
        hp -= damage;
    }

    void HandleState()
    {
        if (targetEnemy != null)
        { 
            if(Vector3.Distance(targetEnemy.transform.position, transform.position) <= attackRange)
            {
                state = State.attacking;
            }
            else
            {
                state = State.moving;
            }
        }
        else
        {
            state = State.idle;
        }
    }
}
