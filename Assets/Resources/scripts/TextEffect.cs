/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    string str;
    Text tex;
    int i = 0;
    int index = 0;
    string str1 = "";
    bool ison = true;
    private void Start()
    {
        tex = GetComponent<Text>();
        str = tex.text;
        tex.text = "这是一个测试文本";
        i = 15;
    }

    // Update is called once per frame
    private void Update()
    {
        if(ison)
        {
            i -= 1;
            if(i<=0)
            {
                if(index>=str .Length )
                {
                    ison = false;
                    return;
                }
                str1 = str1 + str[index].ToString();
                tex.text = str1;
                index += 1;
                i = 15;
            }
        }
    }
}    //另一种实现
*/


using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{

    public float charsPerSecond = 0.3f;//打字时间间隔
    private string words="";//保存需要显示的文字

    private bool isActive = false;
    private float timer;//计时器
    private Text myText;
    private int currentPos = 0;//当前打字位置

    // Use this for initialization
    void Start()
    {
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(0.2f, charsPerSecond);
        myText = GetComponent<Text>(); //实际输出的是文字框的字
        words = myText.text;
        myText.text = "";//获取Text的文本信息，保存到words中，然后动态更新文本显示内容，实现打字机的效果
    }
    public void OnButton()
    {
        isActive = gameObject.GetComponentInChildren<Toggle>().isOn;
    }

    // Update is called once per frame
    void Update()
    {
        OnStartWriter();
        //Debug.Log (isActive);
    }

    public void StartEffect()
    {
        isActive = true;
    }
    /// <summary>
    /// 执行打字任务
    /// </summary>
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//判断计时器时间是否到达
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//刷新文本显示内容

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// 结束打字，初始化数据
    /// </summary>
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }
}