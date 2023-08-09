using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public List<Vector3> targetVectors = new List<Vector3>();
    private int currentTaget = 0;
    private bool isBackward = false;
    public float waitSecond = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoMove());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CoMove()
    {
        while (true)
        {
            // ��ǥ�������� �����ϴ� �ڵ�
            Vector3 dir = new Vector3();
            // ��ǥ�������� ��ġ�� �����ϴ� �ڵ� X�ุ
            dir = Vector3.MoveTowards(transform.position, targetVectors[currentTaget], moveSpeed * Time.deltaTime);

            // ���� ��ġ���� ����� ������ ġȯ�Ѵ�.
            transform.position = dir;

            // ��ǥ������ �������� ��
            Vector3 checkTarget = targetVectors[currentTaget];

            // 1������ ���
            yield return null;

            if (Vector3.Distance(checkTarget, transform.position) < 0.1f)
            {
                //���ð��� ������.
                yield return new WaitForSeconds(waitSecond);

                if (isBackward == true)
                {
                    // ó���������� �����ϸ� ������ ����
                    if (currentTaget == 0)
                    {
                        isBackward = false;
                        currentTaget = currentTaget + 1;
                    }
                    // ó���������� �������� �ʾҴٸ� �� ��������
                    else
                    {
                        currentTaget = currentTaget - 1;
                    }
                }
                else
                {
                    // ������������ �����ϸ� �ڷ� ���ư���
                    if (currentTaget == (targetVectors.Count - 1))
                    {
                        isBackward = true;
                        currentTaget = currentTaget - 1;
                    }
                    // ������������ �������� �ʾҴٸ� ���� ��������
                    else
                    {
                        currentTaget = currentTaget + 1;
                    }
                }
            }
        }
    }
}
