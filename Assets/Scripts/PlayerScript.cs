using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    int level;
    float exp;
    float nextExp;
    public float hp;

    public Stat maxHP;
    public Stat damage;
    public Stat speed;
    public Stat attackTime;

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
        attackTimer = attackTime.GetValue();
        hp = maxHP.GetValue();
        state = State.idle;
        level = 1;
        exp = 0;
        nextExp = 200;

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
            transform.position = new Vector3(transform.position.x + moveDirection.x * speed.GetValue(), transform.position.y, transform.position.z + moveDirection.z * speed.GetValue());
        }
        
    }

    private void Update()
    {
        UIManager.Instance.SetHPText(level, exp, damage.GetValue(), speed.GetValue(), maxHP.GetValue(), hp, attackTime.GetValue(), attackTimer);

        animator.SetInteger("State", (int)state);

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer <= 0)
        {
            if (state == State.attacking) {
                AttackEnemy(targetEnemy);
                attackTimer = attackTime.GetValue();
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
        enemy.GetComponent<SkeletonScript>().hp -= damage.GetValue();

        // 적 처치 처리.
        if(enemy.GetComponent<SkeletonScript>().hp <= 0)
        {
            exp += enemy.GetComponent<SkeletonScript>().exp;

            if(exp >= nextExp)
            {
                HandleLevelUp();
                nextExp += 200;
            }

            Destroy(enemy);
            EnemyManager.Instance.RemoveSkeletonFromList(enemy);
        }
    }

    void HandleLevelUp()
    {
        level++;
        hp = maxHP.GetValue();
        // do something
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

    public void AddDamage(float plusDamage)
    {
        damage.AddMod(plusDamage);
    }

    public void HealPlayer(float plusHP)
    {
        hp += plusHP;
    }

    public void AddMaxHP(float hp)
    {
        maxHP.AddMod(hp);
    }
}
