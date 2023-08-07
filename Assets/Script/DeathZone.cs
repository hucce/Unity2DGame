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
            // 1. �÷��̾��� HP�� ����߸���.
            collision.GetComponent<Player>().OnHit(damage);
            // 2. �÷��̾ ������������ �ǵ�����.
            collision.transform.position = startPoint.transform.position;
        }
    }
}
