using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject startPoint = null;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // 1. 플레이어의 HP를 떨어뜨린다.
            collision.GetComponent<Player>().OnHit(damage);
            // 2. 플레이어를 시작지점으로 되돌린다.
            collision.transform.position = startPoint.transform.position;
        }
    }
}
