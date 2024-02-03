using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public float damage;
    public float hp;
    public float speed;

    // State ���� ���� �ʿ��� ����.

    Vector3 moveDirection;

    private void Update()
    {
        moveDirection = (GameManager.Instance.PlayerGameObject.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * speed);
    }
}
