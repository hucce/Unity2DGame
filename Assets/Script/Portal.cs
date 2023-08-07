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
            // 스테이지 클리어 UI표시
            UIManager.instance.ShowStageClear();
            // 효과음!
            //GetComponent<AudioSource>().Play();
            Invoke("StageClear", intervalSecond);
        }
    }

    private void StageClear()
    {
        SceneManager.LoadScene(nextStage);
    }
}
