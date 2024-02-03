using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public float damage;
    public float hp;
    public float speed;

    // State 관련 구현 필요할 수도.

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
