using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public float intervalSecond = 1f;
    public string nextStage = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �������� Ŭ���� UIǥ��
            if(nextStage == "GameEnd")
            {
                UIManager.instance.ShowGameClear();
            }
            else
            {
                UIManager.instance.ShowStageClear();
            }
            
            // ȿ����!
            //GetComponent<AudioSource>().Play();
            Invoke("StageClear", intervalSecond);
        }
    }

    private void StageClear()
    {
        if(nextStage == "GameEnd")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextStage);
        }
    }
}
