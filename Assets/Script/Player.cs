using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.6f;
    public float jumpForce = 0.5f;
    public GameObject shotObject = null;
    public Transform shotPosition = null;
    private bool jumping = false;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        // 이동
        float h = Input.GetAxis("Horizontal");
        if (h == 0)
        {
            // 이동을 안하는 경우
            GetComponent<Animator>().SetBool("isRun", false);

        }
        else if(h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Move(h);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Move(h);
        }


        // 점프
        if(jumping == false)
        {
            bool jump = Input.GetButtonDown("Jump");

            if (jump == true)
            {
                Vector2 jumpVector = new Vector2();
                jumpVector.y = jumpForce;

                GetComponent<Animator>().SetBool("isRun", false);
                GetComponent<Animator>().SetBool("isJump", true);
                GetComponent<Rigidbody2D>().AddForce(jumpVector);
                jumping = true;
            }
        }

        // 총쏘기
        bool shot = Input.GetButtonDown("Fire1");
        if(shot == true)
        {
            GameObject.Instantiate(shotObject, shotPosition.position, shotPosition.rotation);
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
        jumping = false;
        GetComponent<Animator>().SetBool("isRun", false);
        GetComponent<Animator>().SetBool("isJump", false);
    }
}
