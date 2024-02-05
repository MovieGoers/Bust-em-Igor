using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonScript : MonoBehaviour
{
    public float damage;
    public float hp;
    public float speed;
    public float attackRange;

    GameObject player;

    public GameObject hpBar;
    Slider hpBarSlider;

    Vector3 moveDirection;

    enum State
    {
        idle,
        moving,
        attacking
    }

    State state;

    private void Start()
    {
        hpBarSlider = hpBar.GetComponent<Slider>();
        player = GameManager.Instance.PlayerGameObject;

        hpBarSlider.minValue = 0;
        hpBarSlider.maxValue = hp;

        state = State.idle;
    }

    private void Update()
    {
        HandleState();

        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        hpBarSlider.value = hp;
    }

    private void FixedUpdate()
    {

        moveDirection = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        if (state == State.moving)
            transform.position = new Vector3(transform.position.x + moveDirection.x * speed, transform.position.y, transform.position.z + moveDirection.z * speed);
        else if(state == State.attacking)
        {
            player.GetComponent<PlayerScript>().AttackPlayer(damage);
        }
    }

    void HandleState()
    {
        if(player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
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
