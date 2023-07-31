using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Vector3 AVec = new Vector3();
    public Vector3 BVec = new Vector3();
    private bool ADir = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // A지점, B지점
        // 현재위치, 목표위치, 속도
        if (ADir == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, AVec, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, BVec, moveSpeed * Time.deltaTime);
        }

        if (transform.position == AVec)
        {
            ADir = false;
        }
        else if(transform.position == BVec)
        {
            ADir = true;
        }
    }
}
