using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;



public class test_button : MonoBehaviour
{
    //����һ��ԭʼ�ű���������˲��Դ��룬����ֻ���ڰ�ť���²����ѳ�ʼ�ű�
    //GameObject spine1 = GameObject.Find("test-amiya");
    // Start is called before the first frame update
    //Debug.log("yes");
    void Start()
    {
        //spine1.GetComponent<skeletonAnimation>();
        //Debug.Log("111");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void testhanshu()
    {
        //Debug.Log("test");
        SkeletonAnimation test1 = GameObject.Find("amiya").GetComponent<SkeletonAnimation>();
        
        SkeletonDataAsset newAsset= Resources.Load<SkeletonDataAsset>("spine-assets/player/amiya-attack/char_002_amiya_SkeletonData");
        //resoureces.load���������ǰ�����ļ�������resources����ļ�����
        
        if (newAsset == null) Debug.Log("no asset"); //����Ƿ��ҵ�������Դ
        
        test1.skeletonDataAsset = newAsset;
        newAsset.GetSkeletonData(true);
        

        test1.Initialize(true);
        test1.AnimationName ="Skill_2";
        Debug.Log("test�������н���");       
    }  //���� ����

    public void startGame()
    {
        //����startGame�ű�
        GameObject test = GameObject.Find("���ֽű�����");
        test.GetComponent<startGame>().enabled = true ;
    }

   
    /*public void onClick()
    {
        Debug.Log("aaa");
        SkeletonAnimation test1 = GameObject.Find("test-amiya").GetComponent<SkeletonAnimation>();
        test1.AnimationName = "skill2";
        //Debug.Log("yes");
    }

    private void OnMouseDown()
    {
        Debug.Log("down");
        SkeletonAnimation test1 = GameObject.Find("test-amiya").GetComponent<SkeletonAnimation>();
        test1.AnimationName = "skill2";
        Debug.Log("yes");
    }*/
}
