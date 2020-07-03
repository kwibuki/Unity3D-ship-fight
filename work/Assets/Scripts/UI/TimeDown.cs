using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeDown : MonoBehaviour
{
    #region 单例模式
    public static TimeDown inst;

    private void Awake()
    {
        inst = this;
        //Debug.Log("123");
    }
    #endregion


    public int TotalTime = 90;

    public Text TimeText;//在UI里显示时间
    public Transform loseUI;

    private int mumite;

    private int second;

    void Start()
    {
        //StartCoroutine(startTime());   //运行一开始就进行协程
    }

    public IEnumerator startTime()
    {
        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);//由于开始倒计时，需要经过一秒才开始减去1秒，
                                               //所以要先用yield return new WaitForSeconds(1);然后再进行TotalTime--;运算
            TotalTime--;
            TimeText.text = "Time:" + TotalTime;
            if (TotalTime <= 0)
            {                //如果倒计时剩余总时间为0时，就判为输
                LoadScene();
            }
            mumite = TotalTime / 60; //输出显示分
            second = TotalTime % 60; //输出显示秒
            string length = mumite.ToString();
            if (second >= 10)
            {
                TimeText.text = "0" + mumite + ":" + second;
            }     //如果秒大于10的时候，就输出格式为 00：00
            else
                TimeText.text = "0" + mumite + ":0" + second;      //如果秒小于10的时候，就输出格式为 00：00
        }
    }
    void LoadScene()
    {
        loseUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// 退出游戏时停止协程
    /// </summary>

    public void OnExit()
    {
        StopAllCoroutines();
        TimeText.text = "";
    }

    /// <summary>
    /// 重置倒计时
    /// </summary>
    public void OnReset()
    {
        TotalTime = 300;
        StartCoroutine(startTime());
    }

}