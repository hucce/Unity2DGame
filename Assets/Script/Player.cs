using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float moveSpeed = 0.6f;
    public float jumpForce = 1f;
    public float KnockbackFoce = 10f;
    public float hitSecond = 3f;
    private bool jumpState = false;
    private bool hitState = false;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // 내가 맞지 않았을 때에만 이동과 점프가 작동하도록 함
        if(hitState == false)
        {
            // 캐릭터 이동
            MovePlayer();

            // 점프
            Jump();
        }
    }

    private void Jump()
    {
        // 점프가 이미 실행 중이라면 연속으로 점프하지 못하게 한다.
        if (jumpState == false)
        {
            // 점프키 입력을 받으면 
            if (Input.GetButtonDown("Jump"))
            {
                // Y축으로 힘을 받는다.
                Vector2 force = new Vector2();
                force.y = jumpForce;
                GetComponent<Rigidbody2D>().AddForce(force);

                // 점프 상태로 변경한다.
                jumpState = true;

                // 점프가 되면 모션 변경
                GetComponent<Animator>().SetBool("isJump", true);
            }
        }
    }

    private void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        if (h == 0)
        {
            // 이동을 안하는 경우
            GetComponent<Animator>().SetBool("isRun", false);
        }
        else if (h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Move(h);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Move(h);
        }
    }

    private void Move(float h)
    {
        GetComponent<Animator>().SetBool("isRun", true);
        Vector3 pos = new Vector3();
        pos.x = h * moveSpeed * Time.deltaTime;
        transform.Translate(pos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumpState = false;
            GetComponent<Animator>().SetBool("isJump", false);
        }
        else if(collision.gameObject.tag == "Enemie")
        {
            if(hitState == false)
            {
                StartCoroutine(CoHit(collision));
            }
        }
    }

    // 코루틴
    private IEnumerator CoHit(Collision2D collision)
    {
        // 적에게 맞았다.
        // 적의 공격력만큼 HP가 감소했다.
        hp = hp - collision.gameObject.GetComponent<Enemie>().attackDamage;

        // 플레이어 캐릭터의 애니메이션 변경
        GetComponent<Animator>().SetBool("isHit", true);

        // 1. 맞았을때는 컨트롤 불가
        hitState = true;

        // 내 캐릭터 위치 - 적캐릭터의 위치
        Vector3 moveDic = transform.position - collision.transform.position;

        moveDic = Vector3.Normalize(moveDic) * KnockbackFoce;

        // 2. 맞으면 넉백(내가 충돌한 방향의 반대방향으로 움직이도록)
        GetComponent<Rigidbody2D>().AddForce(moveDic, ForceMode2D.Impulse);

        // 3. 맞으면 히트 상태일때에는 무적
        hitState = true;

        // 특정 시간 동안 히트 상태가 유지되고 특정 시간이 끝나면 히트 상태를 끝낸다.
        yield return new WaitForSeconds(hitSecond);

        hitState=false;
        // 플레이어 캐릭터의 애니메이션 변경
        GetComponent<Animator>().SetBool("isHit", false);
    }
}
