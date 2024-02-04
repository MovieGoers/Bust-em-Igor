using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonScript : MonoBehaviour
{
    public float damage;
    public float hp;
    public float speed;

    GameObject player;
    bool isPlayerNear;

    public GameObject hpBar;
    Slider hpBarSlider;

    // State 관련 구현 필요할 수도.

    Vector3 moveDirection;

    private void Start()
    {
        isPlayerNear = false;
        hpBarSlider = hpBar.GetComponent<Slider>();

        hpBarSlider.minValue = 0;
        hpBarSlider.maxValue = hp;
    }

    private void Update()
    {
        moveDirection = (GameManager.Instance.PlayerGameObject.transform.position - transform.position).normalized;
        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        hpBarSlider.value = hp;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        if (!isPlayerNear)
            transform.position += moveDirection * speed;

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
