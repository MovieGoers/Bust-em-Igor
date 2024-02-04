using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public float damage;
    public float hp;
    public float speed;

    GameObject player;
    bool isPlayerNear;

    // State 관련 구현 필요할 수도.

    Vector3 moveDirection;

    private void Start()
    {
        isPlayerNear = false;
    }

    private void Update()
    {
        moveDirection = (GameManager.Instance.PlayerGameObject.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if(!isPlayerNear)
            transform.Translate(moveDirection * speed);

        if (isPlayerNear)
        {
            player.GetComponent<PlayerScript>().AttackPlayer(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            isPlayerNear = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
