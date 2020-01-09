using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KID : MonoBehaviour {
    #region 欄位
    //列舉
    public enum state
    {
        Start, NotComplete, Complete
    }

    public state _state;

    [Header("對話")]
    public string SayStart = "早安呀~咳咳咳";
    public string SayNotComplete = "咳....咳咳";
    public string SayComplete = "穴穴~好多了";
    [Header("對話速度")]
    public float Speed = 1.1f;
    [Header("任務相關")]
    public bool Complete = false;
    public int CountPlayer = 0;
    public int CountFinish = 5;
    public Text textSay;
    public GameObject gObject;
    #endregion
    public GameObject final;
    public Text cc;
    public Text fin;


    public AudioClip soundSay;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        cc.text = CountPlayer + " / 5";
    }

    // Use this for initialization

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Hui")
            Say();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Hui")
            SayClose();
    }

    /// <summary>
    /// 對話:打字效果
    /// </summary>
    private void Say()
    {
        gObject.SetActive(true);
        if (CountPlayer >= CountFinish) _state = state.Complete;

        switch (_state)
        {
            case state.Start:
                StartCoroutine(ShowDialog(SayStart));
                _state = state.NotComplete;
                break;
            case state.Complete:
                StartCoroutine(ShowDialog(SayComplete));
                final.SetActive(true);
                fin.text = "上課辛苦惹";

                break;
            case state.NotComplete:
                 StartCoroutine(ShowDialog(SayNotComplete));
                break;
        }
    }
    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                              // 清空文字

        for (int i = 0; i < say.Length; i++)            // 迴圈跑對話.長度
        {
            textSay.text += say[i].ToString();         // 累加每個文字
            aud.PlayOneShot(soundSay, 0.6f);           // 播放一次音效(音效片段，音量)
            yield return new WaitForSeconds(Speed);    // 等待
        }
    }
    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        StopAllCoroutines();
        gObject.SetActive(false);
    }
    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        CountPlayer++;
        cc.text = CountPlayer + " / 5";
    }
}
