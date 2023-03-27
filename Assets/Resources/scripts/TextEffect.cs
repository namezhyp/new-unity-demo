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
        tex.text = "����һ�������ı�";
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
}    //��һ��ʵ��
*/


using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{

    public float charsPerSecond = 0.3f;//����ʱ����
    private string words="";//������Ҫ��ʾ������

    private bool isActive = false;
    private float timer;//��ʱ��
    private Text myText;
    private int currentPos = 0;//��ǰ����λ��

    // Use this for initialization
    void Start()
    {
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(0.2f, charsPerSecond);
        myText = GetComponent<Text>(); //ʵ������������ֿ����
        words = myText.text;
        myText.text = "";//��ȡText���ı���Ϣ�����浽words�У�Ȼ��̬�����ı���ʾ���ݣ�ʵ�ִ��ֻ���Ч��
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
    /// ִ�д�������
    /// </summary>
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//�жϼ�ʱ��ʱ���Ƿ񵽴�
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//ˢ���ı���ʾ����

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// �������֣���ʼ������
    /// </summary>
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }
}